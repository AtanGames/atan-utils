using System;
using System.Collections.Generic;
using UnityEngine;

namespace AtanUtils.Extensions
{
    public static class DatatypeExtensions
    {
        public static Dictionary<TKey, HashSet<T>> CloneByKeys<TKey, T>(
            this Dictionary<TKey, HashSet<T>> original,
            IEnumerable<TKey> keys)
        {
            var clone = new Dictionary<TKey, HashSet<T>>();
            foreach (var key in keys)
            {
                if (original.TryGetValue(key, out HashSet<T> value))
                {
                    clone[key] = new HashSet<T>(value);
                }
            }
            return clone;
        }
        
        public static List<T> Clone<T>(this List<T> list)
        {
            return new List<T>(list);
        }
        
        public static T[] CloneArray<T>(this T[] array)
        {
            return (T[])array.Clone();
        }
        
        public static void Resize<T>(this T[] array, int newSize)
        {
            Array.Resize(ref array, newSize);
        }
        
        public static string MillisecondsToTimeString(this long milliseconds)
        {
            var seconds = milliseconds / 1000;
            var minutes = seconds / 60;
            var hours = minutes / 60;
            var days = hours / 24;
            
            var parts = new List<string>();
            if (days > 0)
                parts.Add($"{days} days");
            if (hours % 24 > 0)
                parts.Add($"{hours % 24} hours");
            if (minutes % 60 > 0)
                parts.Add($"{minutes % 60} min");
            if (seconds % 60 > 0 || parts.Count == 0)
                parts.Add($"{seconds % 60} sec");
            
            return string.Join(" ", parts);
        }
        
        public static string MillisecondsToTimeString(this int milliseconds)
        {
            return ((long)milliseconds).MillisecondsToTimeString();
        }
        
        public static float InverseLerp(this Color a, Color b, Color value)
        {
            Vector4 vecA = a;
            Vector4 vecB = b;
            Vector4 vecValue = value;

            Vector4 ab = vecB - vecA;
            
            float abSqrMagnitude = ab.sqrMagnitude;
            if (abSqrMagnitude < Mathf.Epsilon)
            {
                return 0.0f;
            }

            Vector4 av = vecValue - vecA;
            float t = Vector4.Dot(av, ab) / abSqrMagnitude;
            return Mathf.Clamp01(t);
        }
    }
}