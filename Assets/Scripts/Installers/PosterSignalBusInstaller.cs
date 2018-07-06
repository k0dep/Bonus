using Extensions;
using Poster;
using Zenject;

namespace Installers
{
    public class PosterSignalBusInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<MessageRevieverNoSerialize>().AsSingle();
            Container.BindInterfacesAndSelfTo<MessageSenderNoSerialize>().AsSingle();
            Container.BindInterfacesAndSelfTo<MessageBinder>().AsSingle();
        }
    }
}