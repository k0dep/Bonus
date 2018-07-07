using Zenject;
using Services;

namespace Installers
{
    public class FluidFieldDimensionServiceInstaller : MonoInstaller
    {
        public FluidFieldDimensionService FluidFieldDimensionInstance;

        public override void InstallBindings()
        {
            Container.Bind<IFluidFieldDimensionService>().To<FluidFieldDimensionService>().FromInstance(FluidFieldDimensionInstance).AsSingle();
        }
    }
}
