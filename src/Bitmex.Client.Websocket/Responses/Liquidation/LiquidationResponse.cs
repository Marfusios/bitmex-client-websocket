using System.Reactive.Subjects;
using Bitmex.Client.Websocket.Json;
using Bitmex.Client.Websocket.Messages;

namespace Bitmex.Client.Websocket.Responses.Liquidation
{
    public class LiquidationResponse : ResponseBase
    {
        public override MessageType Op => MessageType.Position;

        public Liquidation[] Data { get; set; }

        internal static bool TryHandle(string response, ISubject<LiquidationResponse> subject)
        {
            if (!BitmexJsonSerializer.ContainsValue(response, "liquidation"))
                return false;

            var parsed = BitmexJsonSerializer.Deserialize<LiquidationResponse>(response);
            subject.OnNext(parsed);

            return true;
        }
    }
}
