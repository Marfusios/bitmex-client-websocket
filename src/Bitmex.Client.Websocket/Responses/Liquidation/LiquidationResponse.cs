using System.Reactive.Subjects;
using Bitmex.Client.Websocket.Json;
using Bitmex.Client.Websocket.Messages;
using Newtonsoft.Json.Linq;

namespace Bitmex.Client.Websocket.Responses.Liquidation
{
    public class LiquidationResponse : ResponseBase
    {
        public override MessageType Op => MessageType.Position;

        public Liquidation[] Data { get; set; }

        internal static bool TryHandle(JObject response, ISubject<LiquidationResponse> subject)
        {
            if (response?["table"]?.Value<string>() != "liquidation")
                return false;

            var parsed = response.ToObject<LiquidationResponse>(BitmexJsonSerializer.Serializer);
            subject.OnNext(parsed);

            return true;
        }
    }
}
