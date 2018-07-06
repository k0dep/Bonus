using Models;
using UnityEngine;

namespace Controllers
{
    public interface IEntityController
    {
        IEntityModel Model { get; set; }
        
        void Fall(Vector3 point, float travelTime);
        void Slide(Vector3 point, float slideTime);
        void Fire();
        void SetActive(bool isActive);
        void SetBonus(bool isBonus);
    }
}