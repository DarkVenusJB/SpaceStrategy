using Game.Services;
using Game.Services.TickService;
using Zenject;

namespace Installers
{
    public class GameSceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
           InstallServices();
        }

        private void InstallServices()
        {
           Container.BindInterfacesAndSelfTo<TickService>().AsSingle();
        }
    }
    
   
}