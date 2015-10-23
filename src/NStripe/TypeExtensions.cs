using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NStripe
{
    internal static class TypeExtensions
    {
        internal static bool IsDictionary(this Type t)
        {
            return typeof(IDictionary).IsAssignableFrom(t);
        }
    }
}
