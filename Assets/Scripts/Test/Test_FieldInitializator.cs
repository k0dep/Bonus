using System.Collections;
using Controllers;
using Messages;
using Poster;
using UnityEngine;
using Zenject;

namespace Test
{
    public class Test_FieldInitializator : MonoBehaviour
    {
        [Inject]
        public FieldController FieldController { get; set; }
        
        [Inject]
        public IMessageBinder MessageBinder { get; set; }

        [Inject]
        public void Install()
        {
            MessageBinder.Bind<SpawnEntityMessage>(msg => Debug.Log("Заспавнилась сущность"));
            MessageBinder.Bind<EntityFireMessage>(msg => Debug.Log("Сгорела сущность"));
        }


        public bool autoMode = false;
        public float updateInterval = 1.2f;
        public float spawnInterval = 5.0f;

        private void OnGUI()
        {
            if (FieldController == null)
            {
                return;
            }
            
            GUILayout.BeginArea(new Rect(0, 0, 150, 300));
            
            if (GUILayout.Button("Инициализировать"))
            {
                FieldController.Initialize();
            }
            
            if (GUILayout.Button("Fire"))
            {
                FieldController.FireEntities();
            }
            
            if (GUILayout.Button("Fall"))
            {
                FieldController.FallEntities();
            }
            
            if (GUILayout.Button("Spawn"))
            {
                FieldController.SpawnEntities();
            }
            
            if (GUILayout.Button("Dump"))
            {
                Debug.Log(FieldController.FieldModel.DumpMap());
            }
            
            if (GUILayout.Button("Trurn auto mode"))
            {
                autoMode = !autoMode;
                if (autoMode)
                {
                    StartCoroutine(UpdateCoroutine());
                    StartCoroutine(SpawnCoroutine());
                }
                else
                {
                    StopAllCoroutines();
                }
            }
            
            if (GUILayout.Button("<<<") || Input.GetKeyUp(KeyCode.LeftArrow))
            {
                FieldController.MoveEntity(false);
            }
            
            if (GUILayout.Button(">>>") || Input.GetKeyUp(KeyCode.RightArrow))
            {
                FieldController.MoveEntity(true);
            }
            
            GUILayout.EndArea();
        }

        private IEnumerator UpdateCoroutine()
        {
            while (autoMode)
            {
                FieldController.FireEntities();
                FieldController.FallEntities();
                yield return new WaitForSeconds(updateInterval);
            }
        }
        
        private IEnumerator SpawnCoroutine()
        {
            while (autoMode)
            {
                FieldController.SpawnEntities();
                yield return new WaitForSeconds(spawnInterval);
            }
        }
    }
}