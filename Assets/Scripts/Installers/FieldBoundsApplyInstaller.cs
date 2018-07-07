using Services;
using Zenject;

namespace Assets.Scripts.Installers
{
    public class FieldBoundsApplyInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<FieldBoundsApplyService>().AsTransient();
        }
    }
}
