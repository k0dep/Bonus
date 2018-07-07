using System.Collections.Generic;
using UnityEngine;

namespace Models
{
    public interface IGameFieldModel
    {
        ICollection<IEntityModel> BonusEntities { get; set; }
        IList<IEntityModel> Entities { get; set; }
        IFieldDimensionModel FieldDimension { get; set; }
        IEntityModel MovableEntity { get; set; }

        IEnumerable<IEntityModel> ExceptRow(IEnumerable<IEntityModel> entities, uint row);
        IEnumerable<IEnumerable<IEntityModel>> ExtractMatches(IEntityModel[,] map);
        IEntityModel[,] GenerateEntityMap();
        IEnumerable<IEntityModel> GetFallEntities(IEntityModel[,] map);
        IEnumerable<IEntityModel> GetFrezeEntities();
        IEnumerable<IEntityModel> GetFrezeEntities(IEntityModel[,] map);
        Vector2Int GetMapDimensions(IEntityModel[,] map);
        IEnumerable<IEntityModel> GetRow(uint row);
        SidesPossiblePositions GetSidePosibless(Vector2Int fieldPosition, IEntityModel[,] map);
        bool IsEntityFall(Vector2Int point, IEntityModel[,] map);
        bool IsPointInField(Vector2Int point, Vector2Int dimensions);
        IEntityModel Raycast(IEntityModel[,] map, Vector2Int origin, Vector2Int direction);
        IEntityModel Raycast(Vector2Int origin, Vector2Int direction);
    }
}