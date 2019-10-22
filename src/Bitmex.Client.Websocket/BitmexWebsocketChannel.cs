using System;
using Bitmex.Client.Websocket.Client;

namespace Bitmex.Client.Websocket
{
    /// <summary>
    /// Represents a channel used with multiplexing websockets.
    /// </summary>
    public class BitmexWebsocketChannel
    {
        /// <summary>
        /// Gets the channel identifier.
        /// </summary>
        public Guid ChannelId { get; }

        /// <summary>
        /// Gets or sets the name of the channel.
        /// </summary>
        public string ChannelName { get; }

        /// <summary>
        /// Provided message streams.
        /// </summary>
        public BitmexClientStreams Streams { get; } = new BitmexClientStreams();

        /// <summary>
        /// Initializes a new instance of the <see cref="BitmexWebsocketChannel" /> class.
        /// </summary>
        /// <param name="channelId">The channel identifier.</param>
        /// <param name="channelName">Name of the channel.</param>
        public BitmexWebsocketChannel(Guid channelId, string channelName)
        {
            ChannelId = channelId;
            ChannelName = channelName;
        }
    }
}
