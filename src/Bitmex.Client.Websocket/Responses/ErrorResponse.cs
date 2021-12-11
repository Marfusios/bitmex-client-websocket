using System.Collections.Generic;
using System.Reactive.Subjects;
using Bitmex.Client.Websocket.Json;
using Bitmex.Client.Websocket.Messages;

namespace Bitmex.Client.Websocket.Responses;

public class ErrorResponse : MessageBase
{
    public override MessageType Op => MessageType.Error;

    public double? Status { get; set; }
    public string Error { get; set; }
    public Dictionary<string, object> Meta { get; set; }
    public Dictionary<string, object> Request { get; set; }

    internal static bool TryHandle(string response, ISubject<ErrorResponse> subject)
    {
        if (!BitmexJsonSerializer.ContainsProperty(response, "error"))
            return false;

        var parsed = BitmexJsonSerializer.Deserialize<ErrorResponse>(response);
        subject.OnNext(parsed);
        return true;
    }
}