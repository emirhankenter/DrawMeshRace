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

        public static List<Vector2> Inverse(this List<Vector2> list)
        {
            var tempList = new List<Vector2>();

            for (int i = list.Count - 1; i >= 0; i--)
            {
                tempList.Add(list[i]);
            }

            return tempList;
        }

        public static List<Vector2> Scale(this List<Vector2> list, float factor)
        {
            var tempList = new List<Vector2>();

            foreach (var item in list)
            {
                tempList.Add(item * factor);
            }

            return tempList;
        }
    }
}