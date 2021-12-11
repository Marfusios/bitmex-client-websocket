using System.Reactive.Subjects;
using Bitmex.Client.Websocket.Json;
using Bitmex.Client.Websocket.Messages;

namespace Bitmex.Client.Websocket.Responses.Quotes;

public class QuoteResponse : ResponseBase
{
    public override MessageType Op => MessageType.Quote;

    public Quote[] Data { get; set; }

    internal static bool TryHandle(string response, ISubject<QuoteResponse> subject)
    {
        if (!BitmexJsonSerializer.ContainsValue(response, "quote"))
            return false;

        var parsed = BitmexJsonSerializer.Deserialize<QuoteResponse>(response);
        subject.OnNext(parsed);

        return true;
    }
}