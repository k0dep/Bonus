using System;
using UnityEngine;

namespace Models
{
    [Serializable]
    public class GameConfigModel
    {
        public GameObject[] EntityPtototypes;
        public float FallTime;
        public float SlideTime;
        public uint MaxSpawnUpperCount;
        [Range(0.0f, 1.0f)]
        public float BonusDropRandom;
        public uint MaxBonusCount;
    }
}