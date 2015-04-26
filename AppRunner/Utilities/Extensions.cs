using System;

namespace AppRunner.Utilities
{
    public static class Extensions
    {
        public static string With(this string mask, params object[] args)
        {
            return String.Format(mask, args);
        }
    }
}
