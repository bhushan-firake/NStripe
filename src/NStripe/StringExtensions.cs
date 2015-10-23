using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NStripe
{
    internal static class StringExtensions
    {
        public const long UnixEpoch = 621355968000000000L;
        private static readonly DateTime UnixEpochDateTimeUtc = new DateTime(UnixEpoch, DateTimeKind.Utc);

        public static DateTime FromUnixTime(this long unixTime)
        {
            return UnixEpochDateTimeUtc + TimeSpan.FromSeconds(unixTime);
        }

        internal static string ToJson<T>(this T TObject)
        {
            if (TObject != null)
            {
                using (new JsonSerializerScope())
                {
                    return JsonConvert.SerializeObject(TObject);
                }
            }
            return string.Empty;
        }

        internal static T FromJson<T>(this string json)
        {
            if (string.IsNullOrEmpty(json))
                return default(T);

            using (new JsonSerializerScope())
            {
                return JsonConvert.DeserializeObject<T>(json);
            }
        }

        private const int LowerCaseOffset = 'a' - 'A';

        internal static string ToCamelCase(this string value)
        {
            if (string.IsNullOrEmpty(value)) return value;

            var len = value.Length;

            var newValue = new char[len];

            var firstPart = true;

            for (var i = 0; i < len; ++i)
            {
                var c0 = value[i];

                var c1 = i < len - 1 ? value[i + 1] : 'A';

                var c0isUpper = c0 >= 'A' && c0 <= 'Z';
                var c1isUpper = c1 >= 'A' && c1 <= 'Z';

                if (firstPart && c0isUpper && (c1isUpper || i == 0))
                    c0 = (char)(c0 + LowerCaseOffset);
                else
                    firstPart = false;

                newValue[i] = c0;
            }

            return new string(newValue);
        }

        internal static string ToLowercaseUnderscore(this string value)
        {
            if (String.IsNullOrEmpty(value)) return value;
            value = value.ToCamelCase();

            var sb = new StringBuilder(value.Length);
            foreach (char t in value)
            {
                if (Char.IsDigit(t) || (Char.IsLetter(t) && Char.IsLower(t)) || t == '_')
                {
                    sb.Append(t);
                }
                else
                {
                    sb.Append("_");
                    sb.Append(Char.ToLowerInvariant(t));
                }
            }
            return sb.ToString();
        }

        internal static string ToUrl(this object requestObject)
        {
            var timer = new Stopwatch();
            timer.Start();

            var type = requestObject.GetType();

            var routeAttrbs = type.GetCustomAttributes(typeof(RouteAttribute), false);

            if (routeAttrbs != null && routeAttrbs.Length > 0)
            {
                var route = routeAttrbs[0] as RouteAttribute;

                string templatedUrl = route.Path;
                if (templatedUrl.Contains('{') && templatedUrl.Contains('}'))
                {
                    HashSet<PropertyInfo> propertyInfos;
                    if (!Cache.TypeCache.TryGetValue(type, out propertyInfos))
                    {
                        propertyInfos = new HashSet<PropertyInfo>(type.GetProperties());
                        Cache.TypeCache.TryAdd(type, propertyInfos);
                    }

                    foreach (var propertyInfo in propertyInfos)
                    {
                        var replaceableValue = propertyInfo.GetValue(requestObject, null);

                        if (replaceableValue == null)
                            continue;

                        string replaceableProperty = "{" + propertyInfo.Name.ToCamelCase() + "}";

                        templatedUrl = templatedUrl.Replace(replaceableProperty, replaceableValue.ToString());
                    }
                }

                Console.WriteLine("Url Created in {0} ticks", timer.ElapsedTicks);

                return templatedUrl;
            }
            return string.Empty;
        }
    }
}
