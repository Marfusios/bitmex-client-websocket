using System.Collections.Generic;
using Utf8Json;

namespace Bitmex.Client.Websocket.Json
{
    /// <summary>
    /// Helper class for JSON serialization
    /// </summary>
    public static class BitmexJsonSerializer
    {
        ///// <summary>
        ///// Custom JSON settings
        ///// </summary>
        //public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        //{
        //    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
        //    Formatting = Formatting.None,
        //    Converters = new List<JsonConverter>() { new BitmexStringEnumConverter { CamelCaseText = true } },
        //    ContractResolver = new CamelCasePropertyNamesContractResolver()
        //};

        ///// <summary>
        ///// Custom preconfigured JSON serializer
        ///// </summary>
        //public static readonly JsonSerializer Serializer = JsonSerializer.Create(Settings);

        /// <summary>
        /// Deserialize JSON string data by our configuration
        /// </summary>
        public static T Deserialize<T>(string data)
        {
            return JsonSerializer.Deserialize<T>(data);
        }

        /// <summary>
        /// Serialize object into JSON by our configuration
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string Serialize(object data)
        {
            return JsonSerializer.ToJsonString(data);
        }


        /// <summary>
        /// Return true if selected value is present inside response
        /// </summary>
        public static bool ContainsValue(string response, string value)
        {
            var containsValue = string.IsNullOrEmpty(value) || response.Contains($"\"{value}\"");
            return containsValue;
        }

        /// <summary>
        /// Return true if selected property name is present inside response
        /// </summary>
        public static bool ContainsProperty(string response, string property)
        {
            var containsValue = string.IsNullOrEmpty(property) || response.Contains($"\"{property}\":");
            return containsValue;
        }

        /// <summary>
        /// Return true if selected raw string is present inside response
        /// </summary>
        public static bool ContainsRaw(string response, string value)
        {
            var containsValue = string.IsNullOrEmpty(value) || response.Contains(value);
            return containsValue;
        }
    }
}
