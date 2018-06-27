using Models;
using UnityEngine;

namespace Controllers
{
    public class EntityController : MonoBehaviour, IEntityController
    {
        private Vector3 startPosition;
        private Vector3 endPosition;
        private float fallElapsed = 0;
        private float fallTime = 0;
        
        
        public IEntityModel Model { get; set; }
        
        
        void Start()
        {
            Model.WorldPosition = transform.position;
        }
        
        public void Fall(Vector3 point, float travelTime)
        {
            startPosition = transform.position;
            endPosition = point;
            fallElapsed = 0;
            fallTime = travelTime;
        }

        public void Slide(Vector3 point, float slodeTime)
        {
            throw new System.NotImplementedException();
        }

        public void Fire()
        {
            Destroy(gameObject);
        }


        public void Update()
        {
            Model.WorldPosition = transform.position;
            
            if (fallElapsed >= fallTime)
            {
                return;
            }

            fallElapsed += Time.deltaTime;
            transform.position = Vector3.Lerp(startPosition, endPosition, fallElapsed);
        }
    }
}