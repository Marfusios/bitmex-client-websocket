using System.Reactive.Subjects;
using Bitmex.Client.Websocket.Json;
using Bitmex.Client.Websocket.Messages;
using Newtonsoft.Json.Linq;

namespace Bitmex.Client.Websocket.Responses.Books
{
    public class BookResponse : ResponseBase
    {
        public override MessageType Op => MessageType.OrderBook;

        public BookLevel[] Data { get; set; }

        internal static bool TryHandle(JObject response, ISubject<BookResponse> subject)
        {
            if (response?["table"]?.Value<string>() != "orderBookL2")
                return false;

            var parsed = response.ToObject<BookResponse>(BitmexJsonSerializer.Serializer);
            subject.OnNext(parsed);

            return true;
        }
    }
}
