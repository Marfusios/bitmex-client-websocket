using System.Reactive.Subjects;
using Bitmex.Client.Websocket.Json;
using Bitmex.Client.Websocket.Messages;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Bitmex.Client.Websocket.Responses.TradeBins
{
    /// <summary>
    /// Trades bin response, contains all trades executed in a selected time range
    /// </summary>
    public class TradeBinResponse : ResponseBase
    {
        /// <summary>
        /// Operation type
        /// </summary>
        public override MessageType Op => MessageType.Trade;

        /// <summary>
        /// Trades data
        /// </summary>
        public TradeBin[] Data { get; set; }

        /// <summary>
        /// Size of the bin ('1min', '5min', '1h', etc)
        /// </summary>
        [JsonIgnore]
        public string Size { get; private set; }


        internal static bool TryHandle(JObject response, ISubject<TradeBinResponse> subject)
        {
            var type = response?["table"]?.Value<string>();

            if (string.IsNullOrWhiteSpace(type))
                return false;

            if (!type.Contains("tradeBin"))
                return false;

            var parsed = response.ToObject<TradeBinResponse>(BitmexJsonSerializer.Serializer);
            parsed.Size = type.Replace("tradeBin", string.Empty).Trim();
            subject.OnNext(parsed);

            return true;
        }
    }
}
