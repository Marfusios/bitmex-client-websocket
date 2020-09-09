using System.Reactive.Subjects;
using Bitmex.Client.Websocket.Json;
using Bitmex.Client.Websocket.Messages;

namespace Bitmex.Client.Websocket.Responses.Books
{
    /// <summary>
    /// Order book L2 diff response
    /// </summary>
    public class BookResponse : ResponseBase
    {
        /// <inheritdoc />
        public override MessageType Op => MessageType.OrderBook;

        /// <summary>
        /// Order book updates
        /// </summary>
        public BookLevel[] Data { get; set; }

        internal static bool TryHandle(string response, ISubject<BookResponse> subject, string topicName)
        {
            if (!BitmexJsonSerializer.ContainsValue(response, topicName))
                return false;

            var parsed = BitmexJsonSerializer.Deserialize<BookResponse>(response);
            subject.OnNext(parsed);

            return true;
        }
    }
}
