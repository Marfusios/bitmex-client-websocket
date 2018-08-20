using System.Reactive.Subjects;
using Bitmex.Client.Websocket.Json;
using Bitmex.Client.Websocket.Messages;
using Newtonsoft.Json.Linq;

namespace Bitmex.Client.Websocket.Responses.TradeBins
{
    public class TradeBinResponse : ResponseBase
    {
        public override MessageType Op => MessageType.Trade;

        public TradeBin[] Data { get; set; }

        internal static bool TryHandle(JObject response, ISubject<TradeBinResponse> subject)
        {
            if (response?["table"]?.Value<string>() != "tradeBin1m" &&
                response?["table"]?.Value<string>() != "tradeBin5m" &&
                response?["table"]?.Value<string>() != "tradeBin1h" &&
                response?["table"]?.Value<string>() != "tradeBin1d")
                return false;

            var parsed = response.ToObject<TradeBinResponse>(BitmexJsonSerializer.Serializer);
            subject.OnNext(parsed);

            return true;
        }
    }
}
