using System;
using System.Collections.Generic;
using System.Linq;
using Extensions;
using UnityEngine;

namespace Models
{
    /// <summary>
    /// Модель игрового поля
    /// </summary>
    public class GameFieldModel : IGameFieldModel
    {
        public IList<IEntityModel> Entities { get; set; }
        public IEntityModel MovableEntity { get; set; }
        public IFieldDimensionModel FieldDimension { get; set; }
        public ICollection<IEntityModel> BonusEntities { get; set; }

        public GameFieldModel(IFieldDimensionModel fieldDimension)
        {
            FieldDimension = fieldDimension;
            Entities = new List<IEntityModel>();
            BonusEntities = new List<IEntityModel>();
        }
        
        public IEntityModel[,] GenerateEntityMap()
        {
            var map = new IEntityModel[FieldDimension.Colls, FieldDimension.Rows];
            var dimension = GetMapDimensions(map);

            foreach (var entityController in Entities)
            {
                var fieldPosition = FieldDimension.GetFieldPositionFromWorld(entityController.WorldPosition);
                if (!IsPointInField(fieldPosition, dimension))
                {
                    throw new Exception("Сущность находится вне игрового поля");
                }

                map[fieldPosition.x, fieldPosition.y] = entityController;
            }
            
            return map;
        }
        
        public SidesPossiblePositions GetSidePosibless(Vector2Int fieldPosition, IEntityModel[,] map)
        {
            var dimension = GetMapDimensions(map);
            if (!IsPointInField(fieldPosition, dimension))
            {
                throw new Exception("Точка вне границ игрового поля");
            }

            var result = new SidesPossiblePositions();

            var currentPosition = fieldPosition;

            while (true)
            {
                currentPosition = currentPosition + new Vector2Int(-1, 0);
                if(!IsPointInField(currentPosition, dimension))
                {
                    break;
                }
                
                if (map[currentPosition.x, currentPosition.y] == null)
                {
                    result.Left = currentPosition;
                    break;
                }
            }
            
            currentPosition = fieldPosition;
            
            while (true)
            {
                currentPosition = currentPosition + new Vector2Int(1, 0);
                if(!IsPointInField(currentPosition, dimension))
                {
                    break;
                }
                
                if (map[currentPosition.x, currentPosition.y] == null)
                {
                    result.Right = currentPosition;
                    break;
                }
            }
            
            return result;
        }

        public IEnumerable<IEnumerable<IEntityModel>> ExtractMatches(IEntityModel[,] map)
        {
            var dimensions = GetMapDimensions(map);
            var resultMatches = new List<IEnumerable<IEntityModel>>();
            
            var mapMarkers = new bool[dimensions.x, dimensions.y];
            for (int x = 0; x < dimensions.x; x++)
            {
                for (int y = 0; y < dimensions.y; y++)
                {
                    if (mapMarkers[x, y])
                    {
                        continue;
                    }

                    var passExtract = ExtractSingleMatchAtPoint(new Vector2Int(x, y), map, mapMarkers);

                    if (passExtract.Count() < 3)
                    {
                        continue;
                    }
                    
                    resultMatches.Add(passExtract);
                }
            }

            return resultMatches;
        }

        public IEntityModel Raycast(IEntityModel[,] map, Vector2Int origin, Vector2Int direction)
        {
            var currentPosition = origin;
            var target = currentPosition + direction;
            var mapDimension = GetMapDimensions(map);
            
            while (currentPosition != target)
            {
                currentPosition = currentPosition + direction.ClampOne();
                if(!IsPointInField(currentPosition, mapDimension))
                {
                    break;
                }
                
                if (map[currentPosition.x, currentPosition.y] == null)
                {
                    continue;
                }

                return map[currentPosition.x, currentPosition.y];
            }

            return null;
        }

        public IEntityModel Raycast(Vector2Int origin, Vector2Int direction)
        {
            return Raycast(GenerateEntityMap(), origin, direction);
        }

        protected virtual IEnumerable<IEntityModel> ExtractSingleMatchAtPoint(Vector2Int point, IEntityModel[,] map, bool[,] markers)
        {
            var dimensions = GetMapDimensions(map);
            
            markers[point.x, point.y] = true;
            var entity = map[point.x, point.y];
            if (entity == null)
            {
                return new IEntityModel[0];
            }

            var resultMatch = new List<IEntityModel>() {entity};

            var directions = new List<Vector2Int>()
            {
                new Vector2Int(-1, 0),
                new Vector2Int(1, 0),
                new Vector2Int(0, -1),
                new Vector2Int(0, 1),
            };
            
            foreach (var direction in directions)
            {
                var lookPoint = new Vector2Int(point.x, point.y) + direction;
                if (!IsPointInField(lookPoint, dimensions) || markers[lookPoint.x, lookPoint.y])
                {
                    continue;
                }

                var matchEntity = map[lookPoint.x, lookPoint.y];
                if (matchEntity != null && matchEntity.Type == entity.Type && !IsEntityFall(lookPoint, map))
                {
                    resultMatch.AddRange(ExtractSingleMatchAtPoint(lookPoint, map, markers));
                }
            }

            return resultMatch;
        }


        public IEnumerable<IEntityModel> GetFallEntities(IEntityModel[,] map)
        {
            var dimensions = GetMapDimensions(map);
            var resultEntities = new List<IEntityModel>();

            for (int x = 0; x < dimensions.x; x++)
            {
                for (int y = 0; y < dimensions.y; y++)
                {
                    if (map[x, y] == null)
                    {
                        continue;
                    }

                    if (IsEntityFall(new Vector2Int(x, y), map))
                    {
                        resultEntities.Add(map[x, y]);
                    }
                }
            }

            return resultEntities;
        }

        public IEnumerable<IEntityModel> GetRow(uint row)
        {
            return Entities.Where(entity =>
            {
                var fieldPosition = FieldDimension.GetFieldPositionFromWorld(entity.WorldPosition);
                return fieldPosition.y == row;
            });
        }

        public IEnumerable<IEntityModel> GetFrezeEntities()
        {
            return GetFrezeEntities(GenerateEntityMap());
        }
        
        public IEnumerable<IEntityModel> GetFrezeEntities(IEntityModel[,] map)
        {
            var result = new List<IEntityModel>();
            foreach (var entityModel in Entities)
            {
                if (IsEntityFall(FieldDimension.GetFieldPositionFromWorld(entityModel.WorldPosition), map))
                {
                    continue;
                }
                result.Add(entityModel);
            }
            return result;
        }

        public IEnumerable<IEntityModel> ExceptRow(IEnumerable<IEntityModel> entities, uint row)
        {
            return entities.Where(entity =>
            {
                var fieldPosition = FieldDimension.GetFieldPositionFromWorld(entity.WorldPosition);
                return fieldPosition.y != row;
            });
        }

        public bool IsEntityFall(Vector2Int point, IEntityModel[,] map)
        {
            var dimensions = GetMapDimensions(map);
            
            if (!IsPointInField(point, dimensions))
            {
                return false;
            }

            var downPoint = point + Vector2Int.down;
            if (IsPointInField(downPoint, dimensions) && map[downPoint.x, downPoint.y] == null)
            {
                return true;
            }

            return IsEntityFall(downPoint, map);
        }

        public bool IsPointInField(Vector2Int point, Vector2Int dimensions)
        {
            return !(point.x >= dimensions.x
                   || point.x < 0
                   || point.y >= dimensions.y
                   || point.y < 0);
        }

        public Vector2Int GetMapDimensions(IEntityModel[,] map)
        {
            var width = map.GetLength(0);
            var height = map.GetLength(1);

            return new Vector2Int(width, height);
        }

        public string DumpMap()
        {
            return DumpMap(GenerateEntityMap());
        }

        private string DumpMap(IEntityModel[,] map)
        {
            var result = "";
            var dimension = GetMapDimensions(map);

            for (int y = dimension.y-1; y >= 0; y--)
            {
                for (int x = dimension.x-1; x >= 0; x--)
                {
                    result += map[x, y] == null ? "-" : map[x, y].Type.ToString();
                }

                result += "\n";
            }
            
            return result;
        }

    }
}