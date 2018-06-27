using Models;
using Zenject;

namespace Installers
{
    public class GameFieldModelInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<GameFieldModel>().ToSelf().AsSingle();
        }
    }
}