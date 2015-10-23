using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Web;

namespace NStripe
{
    public class StripeRequestBase
    {
        public override string ToString()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            string queryString = PrepareQueryString(this);
            Console.WriteLine("Query Formed In {0}ticks", stopwatch.ElapsedTicks);
            return queryString;
        }

        public string PrepareQueryString(object @object)
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

                if (IsDictionary(propertyValue.GetType()))
                {
                    var dictionaryValue = (Dictionary<string, string>)propertyValue;
                    foreach (var key in dictionaryValue.Keys)
                    {
                        queryStringBuilder.Append(AppendParam(queryStringBuilder.ToString(), string.Format("{0}[{1}]", propertyNameWithUnderscore, key), dictionaryValue[key]));
                    }
                    continue;
                }

                if (queryStringBuilder.Length > 0)
                    queryStringBuilder.Append("&");

                queryStringBuilder.AppendFormat("{0}={1}", propertyNameWithUnderscore, HttpUtility.UrlEncode(propertyValue.ToString()));
            };
            return queryStringBuilder.ToString();
        }

        public bool IsDictionary(Type t)
        {
            return typeof(IDictionary).IsAssignableFrom(t);
        }

        public string AppendParam(string str, string key, string value)
        {
            return string.Format("{0}{1}={2}", str.Length > 0 ? "&" : string.Empty, key, HttpUtility.UrlEncode(value));
        }
    }
}
