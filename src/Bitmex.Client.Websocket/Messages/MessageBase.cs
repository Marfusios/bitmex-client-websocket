namespace Bitmex.Client.Websocket.Messages;

/// <summary>
/// Message which is used as base for every request and response
/// </summary>
public class MessageBase
{
    /// <summary>
    /// Unique operation, is serialized as "op": "command"
    /// </summary>
    public virtual MessageType Op { get; set; }
}