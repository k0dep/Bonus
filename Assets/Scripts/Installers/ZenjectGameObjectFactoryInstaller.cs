using Factories;
using Zenject;

namespace Installers
{
    public class ZenjectGameObjectFactoryInstaller : MonoInstaller<ZenjectGameObjectFactoryInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<IGameObjectFactory>().To<ZenjectGameObjectFactory>().AsSingle();
        }
    }
}