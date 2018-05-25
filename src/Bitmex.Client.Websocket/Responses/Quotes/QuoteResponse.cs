using System.Reactive.Subjects;
using Bitmex.Client.Websocket.Json;
using Bitmex.Client.Websocket.Messages;
using Newtonsoft.Json.Linq;

namespace Bitmex.Client.Websocket.Responses.Quotes
{
    public class QuoteResponse : ResponseBase
    {
        public override MessageType Op => MessageType.Quote;

        public Quote[] Data { get; set; }

        internal static bool TryHandle(JObject response, ISubject<QuoteResponse> subject)
        {
            if (response?["table"]?.Value<string>() != "quote")
                return false;

            var parsed = response.ToObject<QuoteResponse>(BitmexJsonSerializer.Serializer);
            subject.OnNext(parsed);

            return true;
        }
    }
}
