using System.Collections;
using Controllers;
using UnityEngine;

namespace Test
{
    public class Test_FieldInitializator : MonoBehaviour
    {
        public FieldController FieldController;


        public bool autoMode = false;
        public float updateInterval = 1.2f;
        public float spawnInterval = 5.0f;
        
        void Start()
        {
            FieldController.Initialize();
        }

        private void OnGUI()
        {
            if (GUILayout.Button("Update"))
            {
                FieldController.UpdateField();
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
        }

        private IEnumerator UpdateCoroutine()
        {
            while (autoMode)
            {
                FieldController.UpdateField();
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