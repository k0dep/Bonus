using Controllers;
using UnityEngine;
using Zenject;

namespace Test
{
    public class Test_FieldControllerGizmos : MonoBehaviour
    {
        [Inject]
        public FieldController Controller { get; set; }
        
        public void OnDrawGizmosSelected()
        {
            if (Controller == null)
            {
                return;
            }
            
            if (Controller.FieldDimensionModel == null)
            {
                return;
            }

            var map = Controller.FieldModel.GenerateEntityMap();
            
            for (int x = 0; x < Controller.FieldDimensionModel.Colls; x++)
            {
                for (int y = 0; y < Controller.FieldDimensionModel.Rows; y++)
                {
                    var cubeSize = new Vector3(Controller.FieldDimensionModel.HorizontalFactor, Controller.FieldDimensionModel.VerticalFactor, 0.5f);
                    var position = Controller.FieldDimensionModel.GetFieldWorldPositionCorner(new Vector2Int(x, y));
                    var offset = new Vector3(Controller.FieldDimensionModel.HorizontalFactor / 2, Controller.FieldDimensionModel.VerticalFactor / 2, 0);
                    Gizmos.DrawWireCube(position + offset, cubeSize);

                    var entity = map[x, y];
                    if (entity == null)
                    {
                        continue;
                    }

                    var entityPosition = Controller.FieldDimensionModel.GetFieldPositionFromWorld(entity.WorldPosition);
                    Gizmos.color = new Color(1, 1, 1, 0.3f);
                    Gizmos.DrawCube(Controller.FieldDimensionModel.GetFieldWorldPositionCorner(entityPosition) + offset, cubeSize);
                }
            }

            if (Controller.FieldModel.MovableEntity != null)
            {
                var possibles = Controller.FieldModel.GetSidePosibless(Controller.ActiveEntityUpdatePosition.Value, map);
                
                Gizmos.DrawSphere(Controller.FieldDimensionModel.GetFieldWorldPosition(Controller.ActiveEntityUpdatePosition.Value), 0.3f);
                
                if (possibles.Right.HasValue)
                {
                    Gizmos.DrawSphere(Controller.FieldDimensionModel.GetFieldWorldPosition(possibles.Right.Value), 0.2f);
                }
            
                if (possibles.Left.HasValue)
                {
                    Gizmos.DrawSphere(Controller.FieldDimensionModel.GetFieldWorldPosition(possibles.Left.Value), 0.2f);
                }
            }
        }

    }
}