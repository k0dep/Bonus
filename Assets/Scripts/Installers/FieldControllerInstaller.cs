using Controllers;
using Zenject;

namespace Installers
{
    public class FieldControllerInstaller : MonoInstaller<FieldControllerInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<FieldController>().AsTransient();
        }
    }
}