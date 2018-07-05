using Models;
using Zenject;

namespace Installers
{
    public class GameConfigModelInstaller : MonoInstaller
    {
        public GameConfigModel GameConfig;
        
        public override void InstallBindings()
        {
            Container.Bind<GameConfigModel>().FromInstance(GameConfig).AsSingle();
        }
    }
}