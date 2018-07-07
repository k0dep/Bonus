using UnityEngine;
using Services;
using Models;
using Zenject;

namespace Test
{
    public class Test_FieldDimensionFluidInitializer : MonoBehaviour
    {
        [Inject]
        public IFluidFieldDimensionService fluidFieldDimensionService { get; set; }

        [Inject]
        public IFieldDimensionModel fieldDimensionModel { get; set; }

        public void Start()
        {
            fieldDimensionModel.GameFieldOrigin.position = fluidFieldDimensionService.GetOriginPosition();

            var dimension = fluidFieldDimensionService.GetFieldDimension();
            fieldDimensionModel.FieldWidth = dimension.x;
            fieldDimensionModel.FieldHeight = dimension.y;
        }
    }
}
