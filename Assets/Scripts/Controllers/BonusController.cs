using System.Linq;
using Extensions;
using Messages;
using Models;
using Poster;
using Services;
using UnityEngine;

namespace Controllers
{
    public class BonusController : IBonusController
    {
        public readonly GameFieldModel FieldModel;
        public readonly IFieldDimensionModel FieldDimensionModel;
        public readonly GameConfigModel ConfigModel;
        public readonly IMessageSender MessageSender;
        public readonly IFieldController FieldController;
        public readonly EntityMapperService MapperService;

        private bool isFiring;
        
        public BonusController(GameFieldModel fieldModel,
            GameConfigModel configModel,
            IMessageSender messageSender,
            IFieldDimensionModel fieldDimensionModel,
            IFieldController fieldController,
            EntityMapperService mapperService)
        {
            FieldModel = fieldModel;
            ConfigModel = configModel;
            MessageSender = messageSender;
            FieldDimensionModel = fieldDimensionModel;
            FieldController = fieldController;
            MapperService = mapperService;
        }
        
        
        public void AssignEntityBonus(IEntityModel model)
        {
            if (FieldModel.BonusEntities.Contains(model))
            {
                return;
            }
            
            FieldModel.BonusEntities.Add(model);
            var entityController = MapperService.GetController(model);
            entityController.SetBonus(true);

            MessageSender.Send(new AssignEntityBonusMessage {EntityModel = model});
        }

        public void EntityFired(IEntityModel model)
        {
            if (FieldModel.BonusEntities.Contains(model))
            {
                FieldModel.BonusEntities.Remove(model);
                var entityController = MapperService.GetController(model);
                entityController.SetBonus(false);
            }
            
            if (isFiring)
            {
                return;
            }

            isFiring = true;
            
            var hasBonus = false;

            var bonusEntitiesSnapshot = FieldModel.BonusEntities.ToList();
            
            foreach (var fieldModelBonusEntity in bonusEntitiesSnapshot)
            {
                var firedEntityPosition = FieldDimensionModel.GetFieldPositionFromWorld(model.WorldPosition);
                var position = FieldDimensionModel.GetFieldPositionFromWorld(fieldModelBonusEntity.WorldPosition);
                if ((position + new Vector2Int(0, 1)) != firedEntityPosition)
                {
                    continue;
                }

                hasBonus = true;
            }

            if (hasBonus)
            {
                var bottomRow = FieldModel.GetRow(0).ToList();
                
                MessageSender.Send(new ActivateBonusMessage { FiredEntitiesByBonus = bottomRow });
                
                foreach (var fireEntity in bottomRow)
                {
                    FieldController.FireEntity(fireEntity);
                }
            }
            
            isFiring = false;
        }

        public void SelectRandomEntityBonus()
        {    
            if (Random.value > ConfigModel.BonusDropRandom || FieldModel.BonusEntities.Count > ConfigModel.MaxBonusCount)
            {
                return;
            }

            var freeze = FieldModel.GetFrezeEntities();
            var withoutBottomRows = FieldModel.ExceptRow(freeze, 0);
            
            var randomEntity = withoutBottomRows.Random();
            
            if(randomEntity == null)
            {
                return;
            }

            AssignEntityBonus(randomEntity);
        }
    }
}