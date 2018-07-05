using UnityEngine;

namespace Services
{
    public class ForceService
    {
        public Vector3 Direction { get; set; }
        public float Speed { get; set; }
        public float ForceTime { get; set; }
        public bool IsTimeout
        {
            get { return elapsed > ForceTime || !isInit; }
        }

        private float elapsed = 0;

        private bool isInit;

        public void Apply(Transform target)
        {
            if (!isInit || IsTimeout)
            {
                return;
            }

            var deltaPrecission = 0.0f;
            
            elapsed += Time.deltaTime;

            if (elapsed > ForceTime)
            {
                deltaPrecission = elapsed - ForceTime;
            }

            target.position += Direction * (Time.deltaTime - deltaPrecission) * (Speed / ForceTime);
        }

        public void Set(Vector3 direction, float speed, float forceTime)
        {
            isInit = true;
            elapsed = 0;
            Direction = direction;
            ForceTime = forceTime;
            Speed = speed;
        }
    }
}