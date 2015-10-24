using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;

namespace NStripe
{
    internal class JsonSerializerScope : IDisposable
    {
        private readonly Func<JsonSerializerSettings> holdSerializerSettings;

        public JsonSerializerScope()
        {
            holdSerializerSettings = JsonConvert.DefaultSettings;

            JsonConvert.DefaultSettings = () =>
            {
                return new JsonSerializerSettings()
                 {
                     NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore,
                     ContractResolver = new LowercaseUnderscorePropertyNamesContractResolver(),
                     Converters = new List<JsonConverter>
                     {
                         new StringEnumConverter(),
                         new EpochDateTimeConverter()
                     }
                 };
            };
        }

        public void Dispose()
        {
            JsonConvert.DefaultSettings = holdSerializerSettings;
        }
    }
}
