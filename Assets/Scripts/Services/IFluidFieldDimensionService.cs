using UnityEngine;

namespace Services
{
    public interface IFluidFieldDimensionService
    {
        Vector2 GetFieldDimension();
        Vector3 GetOriginPosition();
    }
}