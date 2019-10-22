using Bitmex.Client.Websocket.Requests;

namespace Bitmex.Client.Websocket.Messages
{
    /// <summary>
    /// Base class for a message using a multiplexing socket.
    /// </summary>
    public class MultiplexingMessageBase
    {
        /// <summary>
        /// Gets or sets the type of the message.
        /// </summary>
        public MultiplexingMessageType MessageType { get; set; }

        /// <summary>
        /// Gets or sets the payload.
        /// </summary>
        public RequestBase Payload { get; set; }
    }
}
