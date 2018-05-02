using System.Reactive.Subjects;
using Bitmex.Client.Websocket.Json;
using Bitmex.Client.Websocket.Messages;
using Newtonsoft.Json.Linq;

namespace Bitmex.Client.Websocket.Responses.Orders
{
    public class OrderResponse : ResponseBase
    {
        public override MessageType Op => MessageType.Order;

        public Order[] Data { get; set; }

        internal static bool TryHandle(JObject response, ISubject<OrderResponse> subject)
        {
            if (response?["table"]?.Value<string>() != "order")
                return false;

            var parsed = response.ToObject<OrderResponse>(BitmexJsonSerializer.Serializer);
            subject.OnNext(parsed);

            return true;
        }
    }
}
