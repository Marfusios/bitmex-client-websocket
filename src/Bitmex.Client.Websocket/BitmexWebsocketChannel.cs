using System;

namespace Bitmex.Client.Websocket
{
    /// <summary>
    /// Represents a channel used with multiplexing websockets
    /// </summary>
    public class BitmexWebsocketChannel
    {
        /// <summary>
        /// Gets the channel identifier.
        /// </summary>
        public Guid ChannelId { get; } = Guid.NewGuid();

        /// <summary>
        /// Gets or sets the name of the channel.
        /// </summary>
        public string ChannelName { get; set; }
    }
}
