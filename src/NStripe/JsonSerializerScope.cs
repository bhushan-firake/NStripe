using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace NStripe
{
    internal class JsonSerializerScope : IDisposable
    {
        private readonly Func<JsonSerializerSettings> _holdSerializerSettings;

        public JsonSerializerScope()
        {
            _holdSerializerSettings = JsonConvert.DefaultSettings;

            JsonConvert.DefaultSettings = () => new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore,
                ContractResolver = new LowercaseUnderscorePropertyNamesContractResolver(),
                Converters = new List<JsonConverter>
                {
                    new StringEnumConverter(),
                    new EpochDateTimeConverter()
                }
            };
        }

        public void Dispose()
        {
            JsonConvert.DefaultSettings = _holdSerializerSettings;
        }
    }
}
