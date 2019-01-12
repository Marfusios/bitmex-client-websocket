namespace Bitmex.Client.Websocket.Messages
{
    /// <summary>
    /// Message which is used as base for every request and response
    /// </summary>
    public class MessageBase
    {
        /// <summary>
        /// Operation identification of the message
        /// </summary>
        public virtual MessageType Op { get; set; }
    }
}
