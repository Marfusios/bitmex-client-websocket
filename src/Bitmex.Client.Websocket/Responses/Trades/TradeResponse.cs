using System.Reactive.Subjects;
using Bitmex.Client.Websocket.Json;
using Bitmex.Client.Websocket.Messages;
using Newtonsoft.Json.Linq;

namespace Bitmex.Client.Websocket.Responses.Trades
{
    public class TradeResponse : ResponseBase
    {
        public override MessageType Op => MessageType.Trade;

        public Trade[] Data { get; set; }

        internal static bool TryHandle(JObject response, ISubject<TradeResponse> subject)
        {
            if (response?["table"]?.Value<string>() != "trade")
                return false;

            var parsed = response.ToObject<TradeResponse>(BitmexJsonSerializer.Serializer);
            subject.OnNext(parsed);

            return true;
        }
    }
}
