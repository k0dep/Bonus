using Controllers;
using Zenject;

namespace Installers
{
    public class BonusControllerInstaller : MonoInstaller<BonusControllerInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<BonusController>().AsSingle();
        }
    }
}