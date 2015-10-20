using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NStripe
{
    internal static class StringExtensions
    {
        internal static string ToJson<T>(this T TObject)
        {
            if (TObject != null)
            {
                return JsonConvert.SerializeObject(TObject);
            }
            return string.Empty;
        }

        internal static T FromJson<T>(this string json)
        {
            if (string.IsNullOrEmpty(json))
                return default(T);

            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
