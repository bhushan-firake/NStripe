using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NStripe
{
    internal static class ObjectExtensions
    {
        public static string ToQueryString(this object @object)
        {
            StringBuilder queryStringBuilder = new StringBuilder();
            HashSet<PropertyInfo> propertyInfos;
            var type = @object.GetType();

            if (!Cache.TypeCache.TryGetValue(type, out propertyInfos))
            {
                propertyInfos = new HashSet<PropertyInfo>(type.GetProperties());
                Cache.TypeCache.TryAdd(type, propertyInfos);
            }

            foreach (var propertyInfo in propertyInfos)
            {
                var propertyValue = propertyInfo.GetValue(@object, null);

                if (propertyValue == null) continue;

                string propertyNameWithUnderscore = propertyInfo.Name.ToLowercaseUnderscore();

                if (propertyValue.GetType().IsDictionary())
                {
                    var dictionaryValue = (Dictionary<string, string>)propertyValue;
                    foreach (var key in dictionaryValue.Keys)
                    {
                        queryStringBuilder.Append(queryStringBuilder.ToString().AppendParam(string.Format("{0}[{1}]", propertyNameWithUnderscore, key), dictionaryValue[key]));
                    }
                    continue;
                }

                if (queryStringBuilder.Length > 0)
                    queryStringBuilder.Append("&");

                queryStringBuilder.AppendFormat("{0}={1}", propertyNameWithUnderscore, propertyValue.ToString().ToUrlEncoded());
            };
            return queryStringBuilder.ToString();
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
