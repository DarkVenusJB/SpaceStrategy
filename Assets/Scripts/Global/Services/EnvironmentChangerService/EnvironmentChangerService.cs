using System;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;
using Utils;

namespace Global.Services.EnvironmentChangerService
{
    public class EnvironmentChangerService : IEnvironmentChangerService
    {
        public EEnvironmentType CurrentEnvironment { get; private set; }

        public EnvironmentChangerService(EEnvironmentType initialEnvironment)
        {
            CurrentEnvironment = initialEnvironment;

            if (SceneManager.GetActiveScene().name != initialEnvironment.ToString())
                throw new Exception("Runtime can be started only from StartScene");
        }

        public void  SetEnvironment(EEnvironmentType environment)
        {
            CurrentEnvironment = environment;

            var currentScene = SceneManager.GetActiveScene();

            if (currentScene.name == CurrentEnvironment.ToString())
            {
                TestUtilsHandler.Instance.DebugMessageShow(TestUtilsHandler.ELogSource.EnvironmentChangerService,"Changing environment", 
                    TestUtilsHandler.ELogColor.Red, $"environment {CurrentEnvironment} already loaded");
                return;
            }

            if(currentScene.name != nameof(EEnvironmentType.StartScene))
                SceneManager.UnloadSceneAsync(currentScene);
            
            SceneManager.LoadSceneAsync(CurrentEnvironment.ToString(), LoadSceneMode.Additive);
            
            
            TestUtilsHandler.Instance.DebugMessageShow(TestUtilsHandler.ELogSource.EnvironmentChangerService,"Changing environment", 
                TestUtilsHandler.ELogColor.Pink, $"new environment {CurrentEnvironment}");
        }
    }
}