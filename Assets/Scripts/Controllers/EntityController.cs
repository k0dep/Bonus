using Models;
using Services;
using UnityEngine;

namespace Controllers
{
    public class EntityController : MonoBehaviour, IEntityController
    {
        private readonly ForceService fallForce = new ForceService();
        private readonly ForceService slideForce = new ForceService();
        
        
        public IEntityModel Model { get; set; }
        
        
        void Start()
        {
            Model.WorldPosition = transform.position;
        }
        
        public void Fall(Vector3 point, float travelTime)
        {
            var lookTarget = point - transform.position;
            fallForce.Set(lookTarget.normalized, lookTarget.magnitude, travelTime);
        }

        public void Slide(Vector3 point, float slideTime)
        {
            if (!slideForce.IsTimeout)
            {
                return;
            }
            
            var lookTarget = point - transform.position;
            slideForce.Set(lookTarget.normalized, lookTarget.magnitude, slideTime);
        }

        public void Fire()
        {
            Destroy(gameObject);
        }

        public void SetActive()
        {
        }


        public void Update()
        {
            Model.WorldPosition = transform.position;
            fallForce.Apply(transform);
            slideForce.Apply(transform);
        }
    }
}