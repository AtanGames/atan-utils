using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

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
        
        public static T RandomElement<T>(this IEnumerable<T> source)
        {
            if (source == null) 
                throw new ArgumentNullException(nameof(source));

            T selected = default!;
            int count = 0;

            foreach (var element in source)
            {
                count++;
             
                if (Random.Range(0, count) == 0)
                    selected = element;
            }

            if (count == 0)
                throw new InvalidOperationException("Sequence contains no elements");

            return selected;
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
        
        public static string AddQueryArguments(this string url, params (string key, string value)[] args)
        {
            if (string.IsNullOrEmpty(url) || args == null || args.Length == 0)
                return url;

            // filter out any null or empty keys
            var pairs = args
                .Where(arg => !string.IsNullOrEmpty(arg.key))
                .Select(arg =>
                    $"{Uri.EscapeDataString(arg.key)}={Uri.EscapeDataString(arg.value ?? string.Empty)}"
                ).ToList();

            if (!pairs.Any())
                return url;

            var separator = url.Contains('?') ? '&' : '?';
            return url + separator + string.Join("&", pairs);
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
        
        public static T ParseEnum<T>(this string value, bool ignoreCase = true) where T : struct
        {
            if (Enum.TryParse<T>(value, ignoreCase, out var result))
                return result;

            throw new ArgumentException($"Unable to parse '{value}' to enum {typeof(T).Name}");
        }
        
        public static T RandomElementByProbability<T>(this IEnumerable<T> source, Func<T, float> weightSelector)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (weightSelector == null) throw new ArgumentNullException(nameof(weightSelector));

            float total = 0f;
            T last = default;
            foreach (var item in source)
            {
                total += weightSelector(item);
                last = item;
            }
            if (total <= 0f) throw new InvalidOperationException("Sum of weights must be positive");

            float r = UnityEngine.Random.value * total;
            foreach (var item in source)
            {
                r -= weightSelector(item);
                if (r <= 0f) return item;
            }

            return last;
        }
        
        public static NativeArray<T> ToNativeArray<T>(this T[] source, Allocator allocator)
            where T : struct
        {
            return new NativeArray<T>(source, allocator);
        }
        
        public static byte[] ToByteArray<T>(this NativeArray<T> array) where T : struct
        {
            int size = UnsafeUtility.SizeOf<T>() * array.Length;
            byte[] result = new byte[size];

            NativeArray<byte> bytes = array.Reinterpret<byte>(UnsafeUtility.SizeOf<T>());
            bytes.CopyTo(result);

            return result;
        }
        
        public static NativeArray<T> ToNativeArray<T>(this byte[] source, Allocator allocator)
            where T : struct
        {
            int elemSize = UnsafeUtility.SizeOf<T>();
            if (source.Length % elemSize != 0)
                throw new ArgumentException($"Byte array length {source.Length} is not a multiple of element size {elemSize}.");

            int length = source.Length / elemSize;
            var array = new NativeArray<T>(length, allocator, NativeArrayOptions.UninitializedMemory);

            var bytes = array.Reinterpret<byte>(elemSize);
            source.CopyTo(bytes);

            return array;
        }
        
        public static byte[] ToByteArray<T>(this T[] array) where T : struct
        {
            return MemoryMarshal.AsBytes(array.AsSpan()).ToArray();
        }
        
        public static T[] Repeat<T>(this T value, int count)
        {
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count), "Count must be non-negative.");

            var array = new T[count];
            for (int i = 0; i < count; i++)
                array[i] = value;
            return array;
        }

        public static float3 ToFloat3(this Color color)
        {
            return new float3(color.r, color.g, color.b);
        }
        
        public static float3 ToFloat3Linear(this Color color)
        {
            return new float3(color.linear.r, color.linear.g, color.linear.b);
        }
        
        public static float Squared (this float v)
        {
            return v * v;
        }
    }
}