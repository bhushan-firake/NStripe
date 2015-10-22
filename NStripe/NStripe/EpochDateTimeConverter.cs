using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NStripe
{
    internal class EpochDateTimeConverter : Newtonsoft.Json.JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(DateTime) || objectType == typeof(DateTime?);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var t = (long)Convert.ToDouble(reader.Value.ToString());
            return t.FromUnixTime();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            long ticks;
            DateTime valueAsDate = (DateTime)value;
            if (valueAsDate != DateTime.MinValue)
            {
                if (value is DateTime)
                {
                    var epoc = new DateTime(1970, 1, 1);
                    var delta = (valueAsDate) - epoc;
                    if (delta.TotalSeconds < 0)
                    {
                        throw new ArgumentOutOfRangeException("Unix epoc starts January 1st, 1970");
                    }
                    ticks = (long)delta.TotalSeconds;
                }
                else
                {
                    throw new Exception("Expected date object value.");
                }
                writer.WriteValue(ticks);
            }
        }
    }
}
