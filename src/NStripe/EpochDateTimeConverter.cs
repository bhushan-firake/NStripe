using System;
using Newtonsoft.Json;

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
            DateTime valueAsDate = (DateTime)value;
            if (valueAsDate == DateTime.MinValue)
                return;

            var epoc = new DateTime(1970, 1, 1);
            var delta = (valueAsDate) - epoc;
            if (delta.TotalSeconds < 0)
            {
                throw new ArgumentOutOfRangeException("Unix epoc starts January 1st, 1970");
            }
            var ticks = (long)delta.TotalSeconds;
            writer.WriteValue(ticks);
        }
    }
}
