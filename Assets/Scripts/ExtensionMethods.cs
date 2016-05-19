using System;
using System.Collections.Generic;
using System.Linq;

namespace BBG
{
    public static class ExtensionMethods
    {
        public static void Shuffle<T>(this IList<T> list)
        {
            var rng = new Random();
            int n = list.Count;
            while (n > 1) {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        // Get a random element
        public static T RandomElement<T>(this IList<T> list){
            var rng = new Random();
            int i = rng.Next(0, list.Count);

            return list[i];
        }

        public static KeyValuePair<TKey, TValue> RandomElement<TKey, TValue>(this IDictionary<TKey, TValue> list){
            var rng = new Random();
            int i = rng.Next(0, list.Count);

            return list.ElementAt(i);
        }
    }
}