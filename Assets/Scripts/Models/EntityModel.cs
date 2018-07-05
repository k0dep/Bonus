using System;
using UnityEngine;

namespace Models
{
    [Serializable]
    public class EntityModel : IEntityModel
    {
        public int Type { get; set; }
        public Vector3 WorldPosition { get; set; }

        public EntityModel(int type)
        {
            Type = type;
            WorldPosition = Vector3.zero;
        }
    }
}