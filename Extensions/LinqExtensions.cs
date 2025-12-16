using System;
using System.Collections.Generic;
using UnityEngine;

namespace AtanUtils.Extensions
{
    /// <summary>
    /// Extension methods for Linq
    /// </summary>
    public static class LinqExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (action == null) throw new ArgumentNullException(nameof(action));

            foreach (var item in source)
            {
                action(item);
            }
        }
        
        public static bool TryGetItem<T>(this List<T> list, Predicate<T> predicate, out T item)
        {
            foreach (var element in list)
            {
                if (predicate(element))
                {
                    item = element;
                    return true;
                }
            }

            item = default;
            return false;
        }
    }
}

