using System;
using System.Collections.Generic;

namespace AppRunner.Utilities
{
    public static class Extensions
    {
        /// <summary>
        /// Syntactic sugar, i.e. String.Format("You are {0}", "Bob" ) becomes "You are {0}".With("Bob")
        /// </summary>
        /// <param name="mask"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static string With(this string mask, params object[] args)
        {
            return String.Format(mask, args);
        }

        /// <summary>
        /// Good old mapcar... 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="action"></param>
        public static void ForEach<T>(this IEnumerable<T> list, Action<T> action)
        {
            foreach (var elt in list)
                action(elt);
        }

        public static V GetOrDefault<K, V>(this Dictionary<K, V> dictionary, K key) where V : new() 
        {
            if (dictionary.ContainsKey(key))
                return dictionary[key];
            else
                return new V();

        }
    }
}
