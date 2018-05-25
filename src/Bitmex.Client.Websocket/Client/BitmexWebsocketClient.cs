using System;
using System.Threading.Tasks;
using Bitmex.Client.Websocket.Json;
using Bitmex.Client.Websocket.Requests;
using Bitmex.Client.Websocket.Responses;
using Bitmex.Client.Websocket.Responses.Books;
using Bitmex.Client.Websocket.Responses.Orders;
using Bitmex.Client.Websocket.Responses.Positions;
using Bitmex.Client.Websocket.Responses.Quotes;
using Bitmex.Client.Websocket.Responses.Trades;
using Bitmex.Client.Websocket.Responses.Wallets;
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

        public Task Send<T>(T request) where T: RequestBase
        {
            BmxValidations.ValidateInput(request, nameof(request));

            var serialized = request.IsRaw ? 
                request.OperationString :
                BitmexJsonSerializer.Serialize(request);
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
                bool handled;
                var messageSafe = (message ?? string.Empty).Trim();

                if (messageSafe.StartsWith("{"))
                {
                    handled = HandleObjectMessage(messageSafe);
                    if (handled)
                        return;
                }

                handled = HandleRawMessage(messageSafe);
                if (handled)
                    return;

                Log.Warning(L($"Unhandled response:  '{messageSafe}'"));
            }
            catch (Exception e)
            {
                Log.Error(e, L("Exception while receiving message"));
            }
        }

        private bool HandleRawMessage(string msg)
        {
            // ********************
            // ADD RAW HANDLERS BELOW
            // ********************

            return
                PongResponse.TryHandle(msg, Streams.PongSubject);
        }

        private bool HandleObjectMessage(string msg)
        {
            var response = BitmexJsonSerializer.Deserialize<JObject>(msg);

            // ********************
            // ADD OBJECT HANDLERS BELOW
            // ********************

            return

                TradeResponse.TryHandle(response, Streams.TradesSubject) ||
                BookResponse.TryHandle(response, Streams.BookSubject) ||
                QuoteResponse.TryHandle(response, Streams.QuoteSubject) ||
                PositionResponse.TryHandle(response, Streams.PositionSubject) ||
                OrderResponse.TryHandle(response, Streams.OrderSubject) ||
                WalletResponse.TryHandle(response, Streams.WalletSubject) ||


                ErrorResponse.TryHandle(response, Streams.ErrorSubject) ||
                SubscribeResponse.TryHandle(response, Streams.SubscribeSubject) ||
                InfoResponse.TryHandle(response, Streams.InfoSubject) ||
                AuthenticationResponse.TryHandle(response, Streams.AuthenticationSubject);
        }
    }
}
