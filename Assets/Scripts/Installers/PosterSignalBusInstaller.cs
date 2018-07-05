using Extensions;
using Poster;
using Zenject;

namespace Installers
{
    public class PosterSignalBusInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<ISerializationProvider>().To<UnitySerializationProvider>().AsSingle();
            Container.BindInterfacesAndSelfTo<MessageReceiver>().AsSingle();
            Container.BindInterfacesAndSelfTo<MessageBinder>().AsSingle();
            Container.BindInterfacesAndSelfTo<MessageSender>().AsSingle();
        }
    }
}