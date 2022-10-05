using System.Collections.Generic;

namespace TadPoleFramework.Extensions
{
    public static class ListExtensions
    {
        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = UnityEngine.Random.Range(0, n);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public static void Shuffle<T>(this IList<T> list, int seed)
        {
            var rng = new System.Random(seed);
            int n = list.Count;

            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public static T GetRandomItem<T>(this IList<T> list, int seed)
        {
            var rng = new System.Random(seed);
            int n = list.Count;

            return list[rng.Next(n)];
        }

        public static Dictionary<T,K> DictReverse<T,K>(this Dictionary<T,K> dict)
        {
            List<T> keys = new List<T>();
            List<K> values = new List<K>();

            foreach (var kvp in dict)
            {
                keys.Add(kvp.Key);
                values.Add(kvp.Value);
            }

            values.Reverse();
            

            Dictionary<T, K>  tempDict = new Dictionary<T, K>();

            for (int i = 0; i < keys.Count; i++)
            {
                tempDict.Add(keys[i], values[i]);
            }

            return tempDict;
        }
    }
}