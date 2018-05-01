using System;
using System.Globalization;
using Bitmex.Client.Websocket.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Bitmex.Client.Websocket.Json
{
    public class UnixDateTimeConverter : DateTimeConverterBase
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var substracted = ((DateTime)value).Subtract(BitmexTime.UnixBase);
            writer.WriteRawValue(substracted.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.Value == null) { return null; }
            return BitmexTime.ConvertToTime((long)reader.Value);
        }
    }
}
