using Extensions;
using Messages;
using Models;
using Poster;
using UnityEngine;

namespace Controllers
{
    public class BonusController
    {
        public readonly GameFieldModel FieldModel;

        public readonly IFieldDimensionModel FieldDimensionModel;

        public readonly GameConfigModel ConfigModel;

        public readonly IMessageSender MessageSender;

        public readonly IFieldController FieldController;

        private bool isFiring;
        
        public BonusController(GameFieldModel fieldModel,
            GameConfigModel configModel,
            IMessageSender messageSender,
            IFieldDimensionModel fieldDimensionModel,
            IFieldController fieldController)
        {
            FieldModel = fieldModel;
            ConfigModel = configModel;
            MessageSender = messageSender;
            FieldDimensionModel = fieldDimensionModel;
            FieldController = fieldController;
        }
        
        
        public void AssignEntityBonus(IEntityModel model)
        {
            if (FieldModel.BonusEntities.Contains(model))
            {
                return;
            }
            
            FieldModel.BonusEntities.Add(model);

            MessageSender.Send(new AssignEntityBonusMessage {EntityModel = model});
        }

        public void EntityFired(IEntityModel model)
        {
            if (isFiring)
            {
                return;
            }

            isFiring = true;
            
            var hasBonus = false;
            
            foreach (var fieldModelBonusEntity in FieldModel.BonusEntities)
            {
                var position = FieldDimensionModel.GetFieldPositionFromWorld(fieldModelBonusEntity.WorldPosition);
                var raycastEntity = FieldModel.Raycast(position, new Vector2Int(0, 1));
                if (raycastEntity == null || raycastEntity != model)
                {
                    continue;
                }

                hasBonus = true;
            }

            if (hasBonus)
            {
                var bottomRow = FieldModel.GetRow(0);
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
            
            AssignEntityBonus(randomEntity);
        }
    }
}