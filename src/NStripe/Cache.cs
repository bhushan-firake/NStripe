using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NStripe
{
    internal static class Cache
    {
        public static ConcurrentDictionary<Type, HashSet<PropertyInfo>> TypeCache = new ConcurrentDictionary<Type, HashSet<PropertyInfo>>();
    }
}
