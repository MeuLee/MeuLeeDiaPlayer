using Meziantou.Framework.WPF.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MeuLeeDiaPlayer.PlaylistHandler.Utils
{
    public static class CollectionExtensions
    {

        /// <summary>
        /// Builds a dictionary from a source, using the source's values as keys. 
        /// </summary>
        /// <typeparam name="K">Dictionary key</typeparam>
        /// <typeparam name="V">Dictionary value</typeparam>
        /// <param name="source">Source collection</param>
        /// <param name="defaultValue">Initial value</param>
        /// <returns>The new dictionary</returns>
        public static Dictionary<K, V> ToDictionary<K, V>(this IEnumerable<K> source, V defaultValue)
        {
            _ = source ?? throw new ArgumentNullException(nameof(source));
            var keys = source.ToList();
            var dict = new Dictionary<K, V>(keys.Count);
            foreach (var key in keys)
            {
                dict.Add(key, defaultValue);
            }
            return dict;
        }

        public static T GetRandomValueInList<T>(this IList<T> list, Random r)
        {
            _ = list ?? throw new ArgumentNullException(nameof(list));
            _ = r ?? throw new ArgumentNullException(nameof(r));
            if (list.IsEmpty()) return default;
            return list[r.Next(list.Count)];
        }

        public static bool IsEmpty<T>(this IEnumerable<T> source)
        {
            _ = source ?? throw new ArgumentNullException(nameof(source));
            return !source.Any();
        }

        public static void AddIfNotNull<T>(this ICollection<T> source, T val)
        {
            _ = source ?? throw new ArgumentNullException(nameof(source));
            if (val is null) return;
            source.Add(val);
        }

        public static bool NotContains<T>(this IEnumerable<T> source, T val) where T: class
        {
            _ = source ?? throw new ArgumentNullException(nameof(source));
            return !source.Any(t => t == val);
        }

        public static ConcurrentObservableCollection<T> ToConcurrentObservableCollection<T>(this IEnumerable<T> source)
        {
            _ = source ?? throw new ArgumentNullException(nameof(source));
            var collection = new ConcurrentObservableCollection<T>();
            foreach(T t in source)
            {
                collection.Add(t);
            }
            return collection;
        }
    }
}
