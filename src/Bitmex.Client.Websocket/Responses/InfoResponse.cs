using System;
using System.Collections.Generic;
using System.Reactive.Subjects;
using Bitmex.Client.Websocket.Json;
using Bitmex.Client.Websocket.Messages;

namespace Bitmex.Client.Websocket.Responses;

public class InfoResponse : MessageBase
{
    public override MessageType Op => MessageType.Info;

    public string Info { get; set; }
    public DateTime? Version { get; set; }
    public DateTime? Timestamp { get; set; }
    public string Docs { get; set; }
    public Dictionary<string, object> Limit { get; set; }

    internal static bool TryHandle(string response, ISubject<InfoResponse> subject)
    {
        if (!BitmexJsonSerializer.ContainsProperty(response, "info"))
            return false;

        var parsed = BitmexJsonSerializer.Deserialize<InfoResponse>(response);
        subject.OnNext(parsed);
        return true;
    }
}