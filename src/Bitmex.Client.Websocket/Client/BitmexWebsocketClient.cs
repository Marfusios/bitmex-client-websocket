using System;
using System.Threading.Tasks;
using Bitmex.Client.Websocket.Communicator;
using Bitmex.Client.Websocket.Json;
using Bitmex.Client.Websocket.Logging;
using Bitmex.Client.Websocket.Requests;
using Bitmex.Client.Websocket.Validations;
using Websocket.Client;
using Bitmex.Client.Websocket.Utils;

namespace Bitmex.Client.Websocket.Client
{
    /// <summary>
    /// Bitmex websocket client.
    /// Use method `Send()` to subscribe to channels.
    /// And `Streams` to subscribe. 
    /// </summary>
    public class BitmexWebsocketClient : IDisposable
    {
        private static readonly ILog Log = LogProvider.GetCurrentClassLogger();

        private readonly IBitmexCommunicator _communicator;
        private readonly IDisposable _messageReceivedSubscription;

        /// <inheritdoc />
        public BitmexWebsocketClient(IBitmexCommunicator communicator)
        {
            BmxValidations.ValidateInput(communicator, nameof(communicator));

            _communicator = communicator;
            _messageReceivedSubscription = _communicator.MessageReceived.Subscribe(HandleMessage);
        }

        /// <summary>
        /// Provided message streams
        /// </summary>
        public BitmexClientStreams Streams { get; } = new BitmexClientStreams();

        /// <summary>
        /// Cleanup everything
        /// </summary>
        public void Dispose()
        {
            _messageReceivedSubscription?.Dispose();
        }

        /// <summary>
        /// Serializes request and sends message via websocket communicator. 
        /// It logs and re-throws every exception. 
        /// </summary>
        /// <param name="request">Request/message to be sent</param>
        public async Task Send<T>(T request) where T: RequestBase
        {
            try
            {
                BmxValidations.ValidateInput(request, nameof(request));

                var serialized = request.IsRaw ? 
                    request.OperationString :
                    BitmexJsonSerializer.Serialize(request);
                await _communicator.Send(serialized).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                Log.Error(e, L($"Exception while sending message '{request}'. Error: {e.Message}"));
                throw;
            }
        }

        /// <summary>
        /// Sends authentication request via websocket communicator
        /// </summary>
        /// <param name="apiKey">Your API key</param>
        /// <param name="apiSecret">Your API secret</param>
        public Task Authenticate(string apiKey, string apiSecret)
        {
            return Send(new AuthenticationRequest(apiKey, apiSecret));
        }

        private string L(string msg)
        {
            return $"[BMX WEBSOCKET CLIENT] {msg}";
        }

        private void HandleMessage(ResponseMessage message)
        {
            try
            {
                bool handled;
                var messageSafe = (message.Text ?? string.Empty).Trim();

                if (messageSafe.StartsWith("{"))
                {
                    handled = BitmexResponseHandler.HandleObjectMessage(messageSafe, Streams);
                    if (handled)
                        return;
                }

                handled = BitmexResponseHandler.HandleRawMessage(messageSafe, Streams);
                if (handled)
                    return;

                Log.Warn(L($"Unhandled response:  '{messageSafe}'"));
            }
            catch (Exception e)
            {
                Log.Error(e, L("Exception while receiving message"));
            }
        }
    }
}
