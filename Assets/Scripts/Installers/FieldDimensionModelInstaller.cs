using Models;
using Zenject;

namespace Installers
{
    public class FieldDimensionModelInstaller : MonoInstaller<FieldDimensionModelInstaller>
    {
        public FieldDimensionModel instance;
        
        public override void InstallBindings()
        {
            Container.Bind<IFieldDimensionModel>().FromInstance(instance).AsSingle();
        }
    }
}