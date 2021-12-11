using Bitmex.Client.Websocket.Messages;

namespace Bitmex.Client.Websocket.Requests;

/// <summary>
/// Raw ping request to get pong response
/// </summary>
public class PingRequest : RequestBase
{
    /// <inheritdoc />
    public override MessageType Operation => MessageType.Ping;

    /// <inheritdoc />
    public override bool IsRaw => true;
}