using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Global.Services.EnvironmentChangerService
{
    public class EnvironmentChangerService : IEnvironmentChangerService
    {
        private const string BootstrapSceneName = "StartScene";

        private readonly SemaphoreSlim _transitionLock = new(1, 1);
        private readonly HashSet<string> _loadedEnvironmentScenes = new();

        public EEnvironmentType CurrentEnvironment { get; private set; }

        public EnvironmentChangerService(EEnvironmentType initialEnvironment)
        {
            var activeScene = SceneManager.GetActiveScene();
            
            if (!activeScene.IsValid() || activeScene.name != BootstrapSceneName)
                throw new InvalidOperationException($"{BootstrapSceneName} must always be loaded.");

            CurrentEnvironment = initialEnvironment;
        }

        public async UniTask SetEnvironment(EEnvironmentType environment)
        {
            await _transitionLock.WaitAsync();

            try
            {
                await SetEnvironmentInternal(environment);
            }
            finally
            {
                _transitionLock.Release();
            }
        }

        private async UniTask SetEnvironmentInternal(EEnvironmentType environment)
        {
            var targetSceneName = GetSceneName(environment);
            var targetScene = SceneManager.GetSceneByName(targetSceneName);

            if (!targetScene.isLoaded)
            {
                if (!Application.CanStreamedLevelBeLoaded(targetSceneName))
                {
                    throw new InvalidOperationException($"Scene '{targetSceneName}' is not available in Build Settings.");
                }

                var loadOperation = SceneManager.LoadSceneAsync(targetSceneName, LoadSceneMode.Additive);

                if (loadOperation == null)
                {
                    throw new InvalidOperationException($"Failed to start loading scene {targetSceneName}.");
                }

                await loadOperation.ToUniTask();

                targetScene = SceneManager.GetSceneByName(targetSceneName);
            }

            if (!targetScene.IsValid() || !targetScene.isLoaded)
                throw new InvalidOperationException($"Scene '{targetSceneName}' was not loaded.");

            _loadedEnvironmentScenes.Add(targetSceneName);
            
            if (!SceneManager.SetActiveScene(targetScene))
                throw new InvalidOperationException($"Failed to make scene {targetSceneName} active.");

            CurrentEnvironment = environment;

            await UnloadTrackedScenesExcept(targetSceneName);

            Debug.Log($"Environment changed to {CurrentEnvironment}. Active scene: {targetScene.name}");
        }

        private async UniTask UnloadTrackedScenesExcept(string activeEnvironmentScene)
        {
            var scenesToCheck = new List<string>(_loadedEnvironmentScenes);

            foreach (var sceneName in scenesToCheck)
            {
                if (sceneName == activeEnvironmentScene)
                    continue;

                var scene = SceneManager.GetSceneByName(sceneName);

                if (!scene.IsValid() || !scene.isLoaded)
                {
                    _loadedEnvironmentScenes.Remove(sceneName);
                    continue;
                }

                var unloadOperation = SceneManager.UnloadSceneAsync(scene);

                if (unloadOperation != null)
                {
                    await unloadOperation.ToUniTask();
                    _loadedEnvironmentScenes.Remove(sceneName);
                }
            }
        }

        private static string GetSceneName(EEnvironmentType environment)
        {
            if (environment == EEnvironmentType.StartScene)
                throw new ArgumentOutOfRangeException(nameof(environment), environment, "StartScene is not an environment.");

            return environment.ToString();
        }
    }
}
