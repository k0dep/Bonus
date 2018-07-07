using Controllers;
using Zenject;

namespace Installers
{
    public class GameControllerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<GameController>().AsSingle();
        }
    }
}
