using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;

namespace NStripe
{
    internal static class Cache
    {
        public static ConcurrentDictionary<Type, HashSet<PropertyInfo>> TypeCache = new ConcurrentDictionary<Type, HashSet<PropertyInfo>>();
    }
}
