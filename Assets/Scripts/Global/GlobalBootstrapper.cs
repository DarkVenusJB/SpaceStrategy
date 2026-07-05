using System;
using Cysharp.Threading.Tasks;
using Global.Services.EnvironmentChangerService;
using Global.Services.WindowProviderService;
using Global.Windows.LoadingWindow;
using UnityEngine;
using Zenject;

namespace Global
{
    public class GlobalBootstrapper : MonoBehaviour
    {
        [SerializeField] private float fakeLoadingTime;
        
        [Inject] private IEnvironmentChangerService _environmentChangerService;
        [Inject] private IWindowProviderService _windowProviderService;
        
        private void Awake()
        {
            Input.multiTouchEnabled = false;
        }

        private void Start()
        {
           StartGameAsync().Forget();
        }

        private async UniTask StartGameAsync()
        {
            await UniTask.NextFrame();
            
            _windowProviderService.ShowWindow<LoadingWindowPresenter>().Forget();
            
            await _environmentChangerService.SetEnvironment(EEnvironmentType.MetaScene);
            await UniTask.Delay(TimeSpan.FromSeconds(fakeLoadingTime));
            
            _windowProviderService.CloseWindow<LoadingWindowPresenter>();
        }
    }
}