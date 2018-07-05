using Services;
using Zenject;

namespace Installers
{
    public class EntityMapperServiceInstaller : MonoInstaller<EntityMapperServiceInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<EntityMapperService>().AsSingle();
        }
    }
}