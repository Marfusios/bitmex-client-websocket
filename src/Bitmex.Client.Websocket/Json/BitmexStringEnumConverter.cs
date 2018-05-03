using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Serilog;

namespace Bitmex.Client.Websocket.Json
{
    public class BitmexStringEnumConverter : StringEnumConverter
    {
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            try
            {
                return base.ReadJson(reader, objectType, existingValue, serializer);
            }
            catch
            {
                Log.Warning($"Can't parse enum, value: {reader.Value}, target type: {objectType}, using default '{existingValue}'");
                return existingValue;
            }
        }
    }
}
