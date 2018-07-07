using System;
using UniRx;
using UnityEngine;

namespace UtilityComponents
{
    public class AutoDestroy : MonoBehaviour
    {
        public float DestroyAfter;

        public void Start()
        {
            Observable
                .Timer(TimeSpan.FromSeconds(DestroyAfter))
                .Subscribe(_ => Destroy(gameObject));
        }
    }
}