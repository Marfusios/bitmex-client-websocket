using System.Reactive.Subjects;
using Bitmex.Client.Websocket.Json;
using Bitmex.Client.Websocket.Messages;

namespace Bitmex.Client.Websocket.Responses.Positions;

public class PositionResponse : ResponseBase
{
    public override MessageType Op => MessageType.Position;

    public Position[] Data { get; set; }

    internal static bool TryHandle(string response, ISubject<PositionResponse> subject)
    {
        if (!BitmexJsonSerializer.ContainsValue(response, "position"))
            return false;

        var parsed = BitmexJsonSerializer.Deserialize<PositionResponse>(response);
        subject.OnNext(parsed);

        return true;
    }
}