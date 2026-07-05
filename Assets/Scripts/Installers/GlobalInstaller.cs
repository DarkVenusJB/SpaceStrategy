using Global.Services;
using Global.Services.EnvironmentChangerService;
using Global.Services.SaveLoadService;
using Global.Services.WindowProviderService;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class GlobalInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Debug.Log("n");
            InstallServices();
        }

        private void InstallServices()
        {
            Container.Bind<SaveLoadService>().AsSingle();
            Container.BindInterfacesAndSelfTo<WindowProviderService>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<EnvironmentChangerService>().AsSingle()
                .WithArguments(EEnvironmentType.StartScene).NonLazy();
        }
    }
}

