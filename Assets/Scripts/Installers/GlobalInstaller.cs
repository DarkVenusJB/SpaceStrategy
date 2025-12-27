using Global.Services;
using Zenject;

namespace Installers
{
    public class GlobalInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            InstallServices();
        }

        private void InstallServices()
        {
            Container.BindInterfacesAndSelfTo<EnvironmentChangerService>().AsSingle();
            Container.Bind<SceneLoaderService>().AsSingle();
            Container.Bind<SaveLoadService>().AsSingle();
        }
    }
}

