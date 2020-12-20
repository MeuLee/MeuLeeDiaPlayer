using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MeuLeeDiaPlayer.Common
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
            if (list.IsEmpty()) return default;
            return list[r.Next(list.Count)];
        }

        public static bool IsEmpty<T>(this IEnumerable<T> source)
        {
            return !source.Any();
        }

        public static void AddIfNotNull<T>(this ICollection<T> source, T val)
        {
            if (val is null) return;
            source.Add(val);
        }
    }

    public static class FileExtensions
    {
        private static readonly string[] _audioExtensions = new string[] { "*.mp3", "*.webm" };

        public static IEnumerable<string> GetAudioOnlyFilesInFolder(string folder)
        {
            return _audioExtensions.SelectMany(ext => Directory.EnumerateFiles(folder, $"*{ext}"));
        }
    }
}
