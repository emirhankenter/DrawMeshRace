using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.Extensions
{
    public static class Extension
    {
        public static Vector2 InversePoint(this Vector2 value, Transform target)
        {
            return target.transform.InverseTransformPoint(value);
        }
    }
}