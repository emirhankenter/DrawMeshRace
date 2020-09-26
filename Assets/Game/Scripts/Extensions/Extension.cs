using System;
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

        public static List<T> ConvertToList<T>(this T[] array)
        {
            var list = new List<T>();
            for (int i = 0; i < array.Length; i++)
            {
                list.Add(array[i]);
            }
            return list;
        }
    }
}