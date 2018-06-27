using Models;
using UnityEngine;

namespace Controllers
{
    public interface IEntityController
    {
        IEntityModel Model { get; set; }
        
        void Fall(Vector3 point, float travelTime);
        void Slide(Vector3 point, float slodeTime);
        void Fire();
    }
}