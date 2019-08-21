using System;
using Bitmex.Client.Websocket.Requests;

namespace Bitmex.Client.Websocket.Messages
{
    /// <summary>
    /// Base class for TODO
    /// </summary>
    public class MultiplexingMessageBase
    {
        /// <summary>
        /// Gets or sets the channel.
        /// </summary>
        public BitmexWebsocketChannel Channel { get; set; }

        /// <summary>
        /// Gets or sets the payload.
        /// </summary>
        public RequestBase Payload { get; set; }
    }
}
