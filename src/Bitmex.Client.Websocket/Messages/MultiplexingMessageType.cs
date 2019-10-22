namespace Bitmex.Client.Websocket.Messages
{
    /// <summary>
    /// Message types for multiplexing messages.
    /// </summary>
    public enum MultiplexingMessageType
    {
        Message = 0,
        Subscribe = 1,
        Unsubscribe = 2
    }
}
