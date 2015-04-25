using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
