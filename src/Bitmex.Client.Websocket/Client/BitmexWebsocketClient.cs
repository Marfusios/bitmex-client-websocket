using System;
using System.Threading.Tasks;
using Bitmex.Client.Websocket.Json;
using Bitmex.Client.Websocket.Requests;
using Bitmex.Client.Websocket.Responses;
using Bitmex.Client.Websocket.Responses.Books;
using Bitmex.Client.Websocket.Responses.Trades;
using Bitmex.Client.Websocket.Validations;
using Bitmex.Client.Websocket.Websockets;
using Newtonsoft.Json.Linq;
using Serilog;

namespace Bitmex.Client.Websocket.Client
{
    public class BitmexWebsocketClient : IDisposable
    {
        private readonly BitmexWebsocketCommunicator _communicator;
        private readonly IDisposable _messageReceivedSubsciption;

        public BitmexWebsocketClient(BitmexWebsocketCommunicator communicator)
        {
            BmxValidations.ValidateInput(communicator, nameof(communicator));

            _communicator = communicator;
            _messageReceivedSubsciption = _communicator.MessageReceived.Subscribe(HandleMessage);
        }

        public BitmexClientStreams Streams { get; } = new BitmexClientStreams();

        public void Dispose()
        {
            _messageReceivedSubsciption?.Dispose();
        }

        public Task Send<T>(T request)
        {
            BmxValidations.ValidateInput(request, nameof(request));

            var serialized = BitmexJsonSerializer.Serialize(request);
            return _communicator.Send(serialized);
        }

        public Task Authenticate(string apiKey, string apiSecret)
        {
            return Send(new AuthenticationRequest(apiKey, apiSecret));
        }

        private string L(string msg)
        {
            return $"[BMX WEBSOCKET CLIENT] {msg}";
        }

        private void HandleMessage(string message)
        {
            try
            {
                var formatted = (message ?? string.Empty).Trim();

                if (formatted.StartsWith("{"))
                {
                    OnObjectMessage(formatted);
                    return;
                }

                Log.Warning(L($"Unhandled response: '{formatted}'"));
            }
            catch (Exception e)
            {
                Log.Error(e, L("Exception while receiving message"));
            }
        }

        private void OnObjectMessage(string msg)
        {
            var parsed = BitmexJsonSerializer.Deserialize<JObject>(msg);

            // ********************
            // ADD HANDLERS BELOW
            // ********************

            InfoResponse.TryHandle(parsed, Streams.InfoSubject);
            TradeResponse.TryHandle(parsed, Streams.TradesSubject);
            BookResponse.TryHandle(parsed, Streams.BookSubject);
        }
    }
}
