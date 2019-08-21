using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Concurrent;
using Bitmex.Client.Websocket.Messages;

namespace Bitmex.Client.Websocket.Utils
{
    public class BitmexChannelService
    {
        private readonly ConcurrentDictionary<Guid, MultiplexingMessageBase> _channels;

        public BitmexChannelService()
        {
            _channels = new ConcurrentDictionary<Guid, MultiplexingMessageBase>();
        }

        public BitmexWebsocketChannel CreateChannel(string channelName)
        {
            return new BitmexWebsocketChannel
            {
                ChannelName =  channelName
            };
        }
    }
}
