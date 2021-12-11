using System.Reactive.Subjects;
using Bitmex.Client.Websocket.Messages;

namespace Bitmex.Client.Websocket.Responses;

public class PongResponse : MessageBase
{
    public override MessageType Op => MessageType.Ping;

    public string Message { get; set; }

    internal static bool TryHandle(string response, ISubject<PongResponse> subject)
    {
        if (response == null)
            return false;

        if (!response.ToLower().Contains("pong"))
            return false;

        var parsed = new PongResponse {Message = response};
        subject.OnNext(parsed);
        return true;
    }
}