using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Concurrent;
using Bitmex.Client.Websocket.Messages;

namespace Bitmex.Client.Websocket.Utils
{
    using Json;
    using Newtonsoft.Json;
    using Requests;

    public class BitmexChannelService
    {
        private readonly ConcurrentDictionary<Guid, BitmexWebsocketChannel> _channels;

        public BitmexChannelService()
        {
            _channels = new ConcurrentDictionary<Guid, BitmexWebsocketChannel>();
        }

        public BitmexWebsocketChannel CreateChannel(string channelName)
        {
            var channel = new BitmexWebsocketChannel(GetNonExistentChannelId(), channelName);
            _channels.TryAdd(channel.ChannelId, channel);
            return channel;
        }

        public string Send(BitmexWebsocketChannel ToChannel, MultiplexingMessageBase multiplexingMessage)
        {
            var message = new List<object>
            {
                (int)multiplexingMessage.MessageType,
                ToChannel.ChannelId,
                ToChannel.ChannelName
            };

            if (multiplexingMessage.Payload != null)
            {
                message.Add(multiplexingMessage.Payload);
            }

            //string serializedPayload = request.IsRaw ?
            //    request.OperationString :
            //    BitmexJsonSerializer.Serialize(request);
            return BitmexJsonSerializer.Serialize(message);
        }

        private Guid GetNonExistentChannelId()
        {
            Guid guid;
            do
            {
                guid = Guid.NewGuid();
            }
            while (_channels.ContainsKey(guid));

            return guid;
        }
    }
}
