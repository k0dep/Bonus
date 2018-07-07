using UnityEngine;

namespace Services
{
    public class FluidFieldDimensionService : MonoBehaviour, IFluidFieldDimensionService
    {
        public Transform UpperRightPoint;
        public Transform LowerLeftPoint;

        public Vector3 Offset;

        public Vector3 GetOriginPosition()
        {
            return LowerLeftPoint.position + Offset;
        }

        public Vector2 GetFieldDimension()
        {
            var upperRightPointOffset = UpperRightPoint.position + Offset;
            var lowerLeftPointOffset = LowerLeftPoint.position + Offset;

            return new Vector2(upperRightPointOffset.x - lowerLeftPointOffset.x, upperRightPointOffset.y - lowerLeftPointOffset.y);
        }
    }
}
