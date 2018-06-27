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
        Vector3 GetFieldWorldPosition(Vector2Int point);
        Vector2Int GetFieldPositionFromWorld(Vector3 worldPosition);
    }
}