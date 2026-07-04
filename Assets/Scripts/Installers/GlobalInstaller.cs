using Global.Services;
using Global.Services.EnvironmentChangerService;
using Global.Services.SaveLoadService;
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
            Container.BindInterfacesAndSelfTo<EnvironmentChangerService>().AsSingle()
                .WithArguments(EEnvironmentType.StartScene).NonLazy();
            Container.Bind<SaveLoadService>().AsSingle();
        }
    }
}

