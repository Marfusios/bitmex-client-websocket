using System.Reactive.Subjects;
using Bitmex.Client.Websocket.Json;
using Bitmex.Client.Websocket.Messages;

namespace Bitmex.Client.Websocket.Responses.Books
{
    public class BookResponse : ResponseBase
    {
        public override MessageType Op => MessageType.OrderBook;

        public BookLevel[] Data { get; set; }

        internal static bool TryHandle(string response, ISubject<BookResponse> subject)
        {
            if (!BitmexJsonSerializer.ContainsValue(response, "orderBookL2"))
                return false;

            var parsed = BitmexJsonSerializer.Deserialize<BookResponse>(response);
            subject.OnNext(parsed);

            return true;
        }
    }
}
