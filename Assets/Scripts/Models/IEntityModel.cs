using UnityEngine;

namespace Models
{
    public interface IEntityModel
    {
        int Type { get; set; }
        Vector3 WorldPosition { get; set; }
    }
}