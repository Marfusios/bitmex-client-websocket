using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using Bitmex.Client.Websocket.Communicator;
using Bitmex.Client.Websocket.Exceptions;
using Bitmex.Client.Websocket.Json;
using Bitmex.Client.Websocket.Logging;
using Bitmex.Client.Websocket.Messages;
using Bitmex.Client.Websocket.Utils;
using Bitmex.Client.Websocket.Validations;
using Websocket.Client;

namespace Bitmex.Client.Websocket.Client
{
    public class BitmexMultiplexClient : IDisposable
    {
        private static readonly ILog Log = LogProvider.GetCurrentClassLogger();

        private readonly ConcurrentDictionary<Guid, BitmexWebsocketChannel> _channels;
        private readonly Subject<BitmexWebsocketChannel> channelSubject = new Subject<BitmexWebsocketChannel>();
        private readonly IBitmexCommunicator _communicator;
        private readonly IDisposable _messageReceivedSubscription;

        public IObservable<BitmexWebsocketChannel> Channels => channelSubject.AsObservable();

        public BitmexMultiplexClient(IBitmexCommunicator communicator)
        {
            BmxValidations.ValidateInput(communicator, nameof(communicator));

            _communicator = communicator;
            _messageReceivedSubscription = _communicator.MessageReceived.Subscribe(HandleMessage);

            _channels = new ConcurrentDictionary<Guid, BitmexWebsocketChannel>();
        }

        /// <summary>
        /// Cleanup everything
        /// </summary>
        public void Dispose()
        {
            _messageReceivedSubscription?.Dispose();
        }

        /// <summary>
        /// Creates a channel.
        /// </summary>
        /// <param name="channelName">Name of the channel.</param>
        /// <returns></returns>
        /// <exception cref="BitmexException">Couldn't add the channel with Id = {channel.ChannelId}.</exception>
        public async Task<BitmexWebsocketChannel> CreateChannel(string channelName)
        {
            var channel = new BitmexWebsocketChannel(GetNonExistentChannelId(), channelName);
            if (!_channels.TryAdd(channel.ChannelId, channel))
            {
                Log.Error($"Exception while creating a channel with Id = {channel.ChannelId}");
                throw new BitmexException($"Couldn't add the channel with Id = {channel.ChannelId}.");
            }

            await Send(channel, new MultiplexingMessageBase
            {
                MessageType = MultiplexingMessageType.Subscribe
            });
            channelSubject.OnNext(channel);
            return channel;
        }

        public async Task Send(BitmexWebsocketChannel toChannel, MultiplexingMessageBase multiplexingMessage)
        {
            var message = new List<object>
            {
                (int)multiplexingMessage.MessageType,
                toChannel.ChannelId,
                toChannel.ChannelName
            };

            if (multiplexingMessage.Payload != null)
            {
                message.Add(multiplexingMessage.Payload);
            }

            var serialized = BitmexJsonSerializer.Serialize(message);
            try
            {
                await _communicator.Send(serialized).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                Log.Error(e, L($"Exception while sending message '{serialized}'. Error: {e.Message}"));
                throw;
            }
        }

        private string L(string msg)
        {
            return $"[BMX WEBSOCKET MULTIPLEX-CLIENT] {msg}";
        }

        private void HandleMessage(ResponseMessage message)
        {
            try
            {
                var messageSafe = (message.Text ?? string.Empty).Trim();

                // index 0 = MessageType
                // index 1 = ChannelId 
                // index 2 = ChannelName
                // index 3 = Payload (nullable)
                var deserialized = BitmexJsonSerializer.Deserialize<List<object>>(messageSafe);
                if (deserialized.Count == 4
                    && Guid.TryParse(deserialized[1].ToString(), out Guid channelGuid)
                    && _channels.TryGetValue(channelGuid, out BitmexWebsocketChannel channel))
                {
                    var payload = deserialized[3].ToString();
                    BitmexResponseHandler.HandleObjectMessage(payload, channel.Streams);
                    return;
                }
                Debug.WriteLine($"Unhandled response:  '{messageSafe}'");
            }
            catch (Exception e)
            {
                Debug.WriteLine("Exception while receiving message");
            }
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
