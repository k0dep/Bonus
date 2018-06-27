using System;
using System.Collections.Generic;
using Extensions;
using Factories;
using Models;
using UnityEngine;
using Zenject;

namespace Controllers
{
    /// <summary>
    /// Контроллер игрового поля
    /// </summary>
    public class FieldController : MonoBehaviour, IFieldController
    {
        public GameObject[] EntityPtototypes;
        public float FallTime = 1.4f;
        public uint MaxSpawnUpperCount = 3;
        
        [Inject]
        public GameFieldModel FieldModel { get; set; }

        [Inject]
        public IFieldDimensionModel FieldDimensionModel { get; set; }

        [Inject]
        public IGameObjectFactory GameObjectFactory { get; set; }


        private readonly Dictionary<IEntityModel, IEntityController> entotyModelToController = new Dictionary<IEntityModel, IEntityController>();
        
        
        public void Initialize()
        {
            GameObject lastRandomPrefab = null;
            for (int i = 0; i < FieldDimensionModel.Colls; i++)
            {
                SpawnEntity(i, 0, ref lastRandomPrefab);
            }
        }

        public void UpdateField()
        {
            var map = FieldModel.GenerateEntityMap();

            var firedEntities = FieldModel.ExtractMatches(map);
            foreach (var firedEntity in firedEntities)
            {
                foreach (var entity in firedEntity)
                {
                    MapController(entity).Fire();
                    FieldModel.Entities.Remove(entity);
                }
            }

            map = FieldModel.GenerateEntityMap();
            
            var fallEntities = FieldModel.GetFallEntities(map);
            foreach (var entity in fallEntities)
            {
                var entityFieldPosition = FieldDimensionModel.GetFieldPositionFromWorld(entity.WorldPosition);
                var downWorldPosition = FieldDimensionModel.GetFieldWorldPosition(entityFieldPosition + Vector2Int.down);
                
                MapController(entity).Fall(downWorldPosition, FallTime);
            }
        }

        public void SpawnEntities()
        {
            var freeUpperCells = new List<int>();
            var map = FieldModel.GenerateEntityMap();
            
            for (int i = 0; i < FieldDimensionModel.Colls; i++)
            {
                if (map[i, FieldDimensionModel.Rows - 1] == null)
                {
                    freeUpperCells.Add(i);
                }
            }

            var freeRandomCells = freeUpperCells.RandomRange((uint) Math.Min(MaxSpawnUpperCount, freeUpperCells.Count));
            GameObject lastSpawnedPrefab = null;
            foreach (var freeRandomCell in freeRandomCells)
            {
                SpawnEntity(freeRandomCell, (int) (FieldDimensionModel.Rows-1), ref lastSpawnedPrefab);
                lastSpawnedPrefab = null;
            }
        }
        
        public void MoveEntity(bool IsRight)
        {
            throw new System.NotImplementedException();
        }

        
        
        private void SpawnEntity(int x, int y, ref GameObject lastRandomPrefab)
        {
            var position = FieldDimensionModel.GetFieldWorldPosition(new Vector2Int(x, y));
            var randomPrefab = EntityPtototypes.Random(lastRandomPrefab);
            lastRandomPrefab = randomPrefab;

            var entity = GameObjectFactory.Instantiate(randomPrefab, position);
            var entityController = entity.GetComponent<IEntityController>();

            var entityModel = new EntityModel(Array.IndexOf(EntityPtototypes, randomPrefab)); //TODO: в фабрику
            entityController.Model = entityModel;

            entotyModelToController[entityModel] = entityController;
            
            FieldModel.Entities.Add(entityModel);
        }

        private IEntityController MapController(IEntityModel model)
        {
            return entotyModelToController[model];
        }
    }


    [Serializable]
    public class EntityData
    {
        [SerializeField]
        public GameObject Prefab;
    }
}
