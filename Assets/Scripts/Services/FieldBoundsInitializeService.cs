using Models;

namespace Services
{
    public class FieldBoundsApplyService : IFieldBoundsApplyService
    {
        public readonly IFluidFieldDimensionService FluidFieldDimensionService;
        public readonly IFieldDimensionModel FieldDimensionModel;

        public FieldBoundsApplyService(IFluidFieldDimensionService fluidFieldDimensionService,
            IFieldDimensionModel fieldDimensionModel)
        {
            FluidFieldDimensionService = fluidFieldDimensionService;
            FieldDimensionModel = fieldDimensionModel;
        }

        public void Apply()
        {
            FieldDimensionModel.GameFieldOrigin.position = FluidFieldDimensionService.GetOriginPosition();

            var dimension = FluidFieldDimensionService.GetFieldDimension();
            FieldDimensionModel.FieldWidth = dimension.x;
            FieldDimensionModel.FieldHeight = dimension.y;
        }
    }
}
