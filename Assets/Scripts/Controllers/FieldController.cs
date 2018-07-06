using System;
using System.Collections.Generic;
using Messages;
using Extensions;
using Factories;
using Models;
using Poster;
using Services;
using UnityEngine;

namespace Controllers
{
    /// <summary>
    /// Контроллер игрового поля
    /// </summary>
    public class FieldController : IFieldController
    {
        public readonly GameConfigModel ConfigModel;

        public readonly GameFieldModel FieldModel;

        public readonly IFieldDimensionModel FieldDimensionModel;

        public readonly IGameObjectFactory GameObjectFactory;

        public readonly IMessageSender MessageSender;

        public readonly EntityMapperService EntityMapperService;

        public Vector2Int? ActiveEntityUpdatePosition
        {
            get
            {
                if (FieldModel.MovableEntity != null)
                {
                    return FieldDimensionModel.GetFieldPositionFromWorld(FieldModel.MovableEntity.WorldPosition);
                }

                return null;
            }
        }


        public FieldController(GameConfigModel configModel,
            GameFieldModel fieldModel,
            IFieldDimensionModel fieldDimensionModel,
            IGameObjectFactory gameObjectFactory,
            IMessageSender messageSender,
            EntityMapperService entityMapperService)
        {
            ConfigModel = configModel;
            FieldModel = fieldModel;
            FieldDimensionModel = fieldDimensionModel;
            GameObjectFactory = gameObjectFactory;
            MessageSender = messageSender;
            EntityMapperService = entityMapperService;
        }
        
        public void Initialize()
        {
            GameObject lastRandomPrefab = null;
            for (int i = 0; i < FieldDimensionModel.Colls; i++)
            {
                SpawnEntity(i, 0, ref lastRandomPrefab);
            }
        }

        public void FireEntities()
        {
            var map = FieldModel.GenerateEntityMap();
            
            var firedEntities = FieldModel.ExtractMatches(map);
            foreach (var firedEntity in firedEntities)
            {
                foreach (var entity in firedEntity)
                {
                    FireEntity(entity);
                }
            }
        }

        public void FireEntity(IEntityModel entity)
        {
            EntityMapperService.GetController(entity).Fire();
            FieldModel.Entities.Remove(entity);
            MessageSender.Send(new EntityFireMessage { Entity = entity });
        }

        public void FallEntities()
        {
            var map = FieldModel.GenerateEntityMap();
            
            var fallEntities = FieldModel.GetFallEntities(map);
            foreach (var entity in fallEntities)
            {
                var entityFieldPosition = FieldDimensionModel.GetFieldPositionFromWorld(entity.WorldPosition);
                var downWorldPosition = FieldDimensionModel.GetFieldWorldPosition(entityFieldPosition + Vector2Int.down);
                
                EntityMapperService.GetController(entity).Fall(downWorldPosition, ConfigModel.FallTime);
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

            var newEntities = new List<IEntityModel>();
            var freeRandomCells = freeUpperCells.RandomRange((uint) Math.Min(ConfigModel.MaxSpawnUpperCount, freeUpperCells.Count));
            GameObject lastSpawnedPrefab = null;
            foreach (var freeRandomCell in freeRandomCells)
            {
                var newEntity = SpawnEntity(freeRandomCell, (int) (FieldDimensionModel.Rows - 1),
                    ref lastSpawnedPrefab);
                newEntities.Add(newEntity);
                lastSpawnedPrefab = null;
            }

            if (FieldModel.MovableEntity != null)
            {
                var movableController = EntityMapperService.GetController(FieldModel.MovableEntity);
                movableController.SetActive(false);
            }
            FieldModel.MovableEntity = newEntities.Random();
            var activEntityController = EntityMapperService.GetController(FieldModel.MovableEntity);
            activEntityController.SetActive(true);
        }
        
        public void MoveEntity(bool IsRight)
        {
            if (FieldModel.MovableEntity == null)
            {
                return;
            }
            
            var map = FieldModel.GenerateEntityMap();
            var possibles = FieldModel.GetSidePosibless(ActiveEntityUpdatePosition.Value, map);

            var localOffset = FieldDimensionModel.GetFieldWorldPosition(ActiveEntityUpdatePosition.Value) - FieldModel.MovableEntity.WorldPosition;
            var activeEntityController = EntityMapperService.GetController(FieldModel.MovableEntity);
            
            if (IsRight && possibles.Right.HasValue)
            {
                activeEntityController.Slide(FieldDimensionModel.GetFieldWorldPosition(possibles.Right.Value) - localOffset, ConfigModel.SlideTime);
            }
            
            if (!IsRight && possibles.Left.HasValue)
            {
                activeEntityController.Slide(FieldDimensionModel.GetFieldWorldPosition(possibles.Left.Value) - localOffset, ConfigModel.SlideTime);
            }
        }

        private IEntityModel SpawnEntity(int x, int y, ref GameObject lastRandomPrefab)
        {
            var position = FieldDimensionModel.GetFieldWorldPosition(new Vector2Int(x, y));
            var randomPrefab = ConfigModel.EntityPtototypes.Random(lastRandomPrefab);
            lastRandomPrefab = randomPrefab;

            var entity = GameObjectFactory.Instantiate(randomPrefab, position);
            var entityController = entity.GetComponent<IEntityController>();

            var entityModel = new EntityModel(Array.IndexOf(ConfigModel.EntityPtototypes, randomPrefab)); //TODO: в фабрику
            entityController.Model = entityModel;

            FieldModel.Entities.Add(entityModel);
            EntityMapperService.Add(entityModel, entityController);

            MessageSender.Send(new SpawnEntityMessage{ Entity = entityModel});

            return entityModel;
        }
    }


    [Serializable]
    public class EntityData
    {
        [SerializeField]
        public GameObject Prefab;
    }
}
