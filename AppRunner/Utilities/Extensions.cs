using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

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

        public static string Join(this IEnumerable<object> list, string glue)
        {
            return String.Join(glue, list);
        }


        public static bool IsEmpty<T>(this IEnumerable<T> list)
        {
            return list.Count() == 0;
        }

        public static bool IsNotEmpty<T>(this IEnumerable<T> list)
        {
            return !IsEmpty(list);
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

        public static string ExpandCommandLine(string input)
        {
            var replacements = new List<Tuple<string, Func<string, string>>>()
            {
                Tuple.Create<string, Func<string, string>>("{d:(.*?)}", arg => DateTime.Today.ToString(arg as string)),
                Tuple.Create<string, Func<string, string>>("{d-1:(.*?)}", arg => ( DateTime.Today.AddDays(-1)).ToString(arg as string)),
                Tuple.Create<string, Func<string, string>>(@"{user}", arg => Environment.UserName)
            };

            var args = input;

            replacements.ForEach(tuple =>
                {
                    var regex = new Regex(tuple.Item1);
                    Match match = regex.Match(args);
                    while (match.Success)
                    {
                        var mask = match.Value;
                        var arg = match.Groups[1].Value;
                        match = match.NextMatch();
                        // replace the item1 with item2 applied to match
                        args = args.Replace(mask, tuple.Item2(arg));
                    }
                });

            return args;
        }



    }
}
