﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using Newtonsoft.Json;

namespace NStripe
{
    internal static class StringExtensions
    {
        public const long UnixEpoch = 621355968000000000L;
        private static readonly DateTime UnixEpochDateTimeUtc = new DateTime(UnixEpoch, DateTimeKind.Utc);

        internal static DateTime FromUnixTime(this long unixTime)
        {
            return UnixEpochDateTimeUtc + TimeSpan.FromSeconds(unixTime);
        }

        internal static string ToJson<T>(this T TObject)
        {
            if (TObject == null)
                return string.Empty;

            using (new JsonSerializerScope())
            {
                return JsonConvert.SerializeObject(TObject);
            }
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

                var c0IsUpper = c0 >= 'A' && c0 <= 'Z';
                var c1IsUpper = c1 >= 'A' && c1 <= 'Z';

                if (firstPart && c0IsUpper && (c1IsUpper || i == 0))
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
                if (char.IsDigit(t) || (char.IsLetter(t) && char.IsLower(t)) || t == '_')
                {
                    sb.Append(t);
                }
                else
                {
                    sb.Append("_");
                    sb.Append(char.ToLowerInvariant(t));
                }
            }
            return sb.ToString();
        }

        internal static string ToUrlEncoded(this string str)
        {
            return string.IsNullOrEmpty(str) ? "" : HttpUtility.UrlEncode(str);
        }

        internal static string AppendParam(this string str, string key, string value)
        {
            return string.Format("{0}{1}={2}", str.Length > 0 ? "&" : string.Empty, key, value.ToUrlEncoded());
        }
    }
}
