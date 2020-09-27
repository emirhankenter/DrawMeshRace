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

        public static T First<T>(this T[] array)
        {
            return array[0];
        }
        public static T First<T>(this List<T> list)
        {
            return list[0];
        }

        public static T Last<T>(this T[] array)
        {
            return array[array.Length - 1];
        }
        public static T Last<T>(this List<T> list)
        {
            return list[list.Count - 1];
        }

        public static float RadianToDegree(this float raidans)
        {
            return (180 / Mathf.PI) * raidans;
        }
    }
}