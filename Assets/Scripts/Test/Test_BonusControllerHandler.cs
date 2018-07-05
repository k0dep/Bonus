using Controllers;
using Messages;
using Models;
using Poster;
using UnityEngine;
using Zenject;

namespace Test
{
    public class Test_BonusControllerHandler : MonoBehaviour
    {
        [Inject]
        public GameFieldModel FieldModel { get; set; }
        
        [Inject]
        public BonusController Controller { get; set; }
        
        [Inject]
        public IMessageBinder MessageBinder { get; set; }

        [Inject]
        public void Install()
        {
            MessageBinder.Bind<AssignEntityBonusMessage>(msg => Debug.Log("Выбрана бонучкая сущность"));
            MessageBinder.Bind<EntityFireMessage>(msg => Controller.EntityFired(msg.Entity));
        }
        
        void OnGUI()
        {
            GUILayout.BeginArea(new Rect(160, 0, 150, 300));

            if (GUILayout.Button("Испытать удачу"))
            {
                Controller.SelectRandomEntityBonus();
            }
            
            GUILayout.EndArea();
        }

        private void OnDrawGizmos()
        {
            if (FieldModel == null)
            {
                return;
            }
            
            
            Gizmos.color = new Color(0, 1, 0, 0.3f);
            foreach (var entity in FieldModel.BonusEntities)
            {
                Gizmos.DrawSphere(entity.WorldPosition, 0.4f);
            }
        }
    }
}