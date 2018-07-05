using System;
using UnityEngine;

namespace Extensions
{
    public static class Vector2IntExtensions
    {
        public static Vector2Int ClampOne(this Vector2Int source)
        {
            var result = new Vector2Int();
            result.x = Mathf.Clamp(source.x, -1, 1);
            result.y = Mathf.Clamp(source.y, -1, 1);
            return result;
        }
    }
}