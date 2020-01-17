using System.Reactive.Subjects;
using Bitmex.Client.Websocket.Json;
using Bitmex.Client.Websocket.Messages;

namespace Bitmex.Client.Websocket.Responses.Orders
{
    public class OrderResponse : ResponseBase
    {
        public override MessageType Op => MessageType.Order;

        public Order[] Data { get; set; }

        internal static bool TryHandle(string response, ISubject<OrderResponse> subject)
        {
            if (!BitmexJsonSerializer.ContainsValue(response, "order"))
                return false;

            var parsed = BitmexJsonSerializer.Deserialize<OrderResponse>(response);
            subject.OnNext(parsed);

            return true;
        }
    }
}
