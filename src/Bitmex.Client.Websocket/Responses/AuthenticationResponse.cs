using System.Reactive.Subjects;
using Bitmex.Client.Websocket.Json;
using Bitmex.Client.Websocket.Messages;

namespace Bitmex.Client.Websocket.Responses;

public class AuthenticationResponse : MessageBase
{
    public override MessageType Op => MessageType.AuthKey;

    public bool Success { get; set; }

    internal static bool TryHandle(string response, ISubject<AuthenticationResponse> subject)
    {
        if (!BitmexJsonSerializer.ContainsValue(response, "authKey"))
            return false;

        var parsed = BitmexJsonSerializer.Deserialize<AuthenticationResponse>(response);
        subject.OnNext(parsed);
        return true;
    }
}