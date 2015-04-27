using System;
using System.Collections.Generic;

namespace AppRunner.Utilities
{
    public static class Extensions
    {
        public static string With(this string mask, params object[] args)
        {
            return String.Format(mask, args);
        }

        public static void ForEach<T>(this IEnumerable<T> list, Action<T> action)
        {
            foreach (var elt in list)
                action(elt);
        }
    }
}
