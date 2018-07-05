using UnityEngine;

namespace Models
{
    public interface IFieldDimensionModel
    {
        Transform GameFieldOrigin { get; set; }
        float FieldWidth { get; set; }
        float FieldHeight { get; set; }
        
        uint Rows { get; set; }
        uint Colls { get; set; }
        
        float HorizontalFactor { get; }
        float VerticalFactor { get; }
        
        Vector3 CenterOffset { get; }

        Vector3 GetFieldWorldPositionCorner(Vector2Int point);
        Vector3 GetFieldWorldPosition(Vector2Int point);
        Vector2Int GetFieldPositionFromWorld(Vector3 worldPosition);
    }
}