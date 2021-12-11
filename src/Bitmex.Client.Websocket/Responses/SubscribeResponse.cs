using System.Reactive.Subjects;
using Bitmex.Client.Websocket.Json;
using Bitmex.Client.Websocket.Messages;

namespace Bitmex.Client.Websocket.Responses;

/// <summary>
/// Response for every subscribe and unsubscribe event
/// </summary>
public class SubscribeResponse : MessageBase
{
    /// <inheritdoc />
    public override MessageType Op => MessageType.Subscribe;

    /// <summary>
    /// Subscribed topics
    /// </summary>
    public string Subscribe { get; set; }

    /// <summary>
    /// Unsubscribed topics
    /// </summary>
    public string Unsubscribe { get; set; }

    /// <summary>
    /// Returns true if subscribe or unsubscribe succeed
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// Returns true if it is subscription event
    /// </summary>
    public bool IsSubscription => !string.IsNullOrWhiteSpace(Subscribe);

    internal static bool TryHandle(string response, ISubject<SubscribeResponse> subject)
    {
        if (!BitmexJsonSerializer.ContainsProperty(response, "subscribe") && 
            !BitmexJsonSerializer.ContainsProperty(response, "unsubscribe"))
            return false;

        var parsed = BitmexJsonSerializer.Deserialize<SubscribeResponse>(response);
        subject.OnNext(parsed);
        return true;
    }
}