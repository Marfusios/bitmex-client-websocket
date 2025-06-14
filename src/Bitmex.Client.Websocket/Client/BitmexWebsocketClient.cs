using System;
using Bitmex.Client.Websocket.Communicator;
using Bitmex.Client.Websocket.Json;
using Bitmex.Client.Websocket.Requests;
using Bitmex.Client.Websocket.Responses;
using Bitmex.Client.Websocket.Responses.Books;
using Bitmex.Client.Websocket.Responses.Liquidation;
using Bitmex.Client.Websocket.Responses.Orders;
using Bitmex.Client.Websocket.Responses.Positions;
using Bitmex.Client.Websocket.Responses.Quotes;
using Bitmex.Client.Websocket.Responses.Trades;
using Bitmex.Client.Websocket.Responses.TradeBins;
using Bitmex.Client.Websocket.Responses.Wallets;
using Bitmex.Client.Websocket.Validations;
using Bitmex.Client.Websocket.Responses.Instruments;
using Bitmex.Client.Websocket.Responses.Margins;
using Websocket.Client;
using Bitmex.Client.Websocket.Responses.Executions;
using Bitmex.Client.Websocket.Responses.Fundings;
using Utf8Json;
using Utf8Json.Resolvers;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace Bitmex.Client.Websocket.Client
{
    /// <summary>
    /// Bitmex websocket client.
    /// Use method `Send()` to subscribe to channels.
    /// And `Streams` to subscribe. 
    /// </summary>
    public class BitmexWebsocketClient : IDisposable
    {
        private readonly IBitmexCommunicator _communicator;
        private readonly IDisposable _messageReceivedSubscription;
        private readonly ILogger<BitmexWebsocketClient> _logger;

        /// <summary>
        /// Creates an instance of BitmexWebsocketClient
        /// </summary>
        public BitmexWebsocketClient(IBitmexCommunicator communicator, ILogger<BitmexWebsocketClient>? logger = null)
        {
            BmxValidations.ValidateInput(communicator, nameof(communicator));

            _communicator = communicator;
            _logger = logger ?? NullLogger<BitmexWebsocketClient>.Instance;
            _messageReceivedSubscription = _communicator.MessageReceived.Subscribe(HandleMessage);

            JsonSerializer.SetDefaultResolver(StandardResolver.CamelCase);
        }

        /// <summary>
        /// Provided message streams
        /// </summary>
        public BitmexClientStreams Streams { get; } = new BitmexClientStreams();

        /// <summary>
        /// Expose logger for this client
        /// </summary>
        public ILogger<BitmexWebsocketClient> Logger => _logger;

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
        public void Send<T>(T request) where T : RequestBase
        {
            try
            {
                BmxValidations.ValidateInput(request, nameof(request));

                var serialized = request.IsRaw ?
                    request.OperationString :
                    BitmexJsonSerializer.Serialize(request);
                _communicator.Send(serialized);
            }
            catch (Exception e)
            {
                _logger.LogError(e, L("Exception while sending message '{request}'. Error: {error}"), request, e.Message);
                throw;
            }
        }

        /// <summary>
        /// Sends authentication request via websocket communicator
        /// </summary>
        /// <param name="apiKey">Your API key</param>
        /// <param name="apiSecret">Your API secret</param>
        public void Authenticate(string apiKey, string apiSecret)
        {
            Send(new AuthenticationRequest(apiKey, apiSecret));
        }

        private string L(string msg)
        {
            return $"[BMX WEBSOCKET CLIENT] {msg}";
        }

        protected virtual void HandleMessage(ResponseMessage message)
        {
            try
            {
                bool handled;
                var messageSafe = (message.Text ?? string.Empty).Trim();

                if (messageSafe.StartsWith("{"))
                {
                    handled = HandleObjectMessage(messageSafe);
                    if (handled)
                        return;
                }

                handled = HandleRawMessage(messageSafe);
                if (handled)
                    return;

                if (!string.IsNullOrWhiteSpace(messageSafe))
                    Streams.UnhandledMessageSubject.OnNext(messageSafe);
                _logger.LogWarning(L("Unhandled response:  '{message}'"), messageSafe);
            }
            catch (Exception e)
            {
                _logger.LogError(e, L("Exception while receiving message, error: {error}"), e.Message);
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
            // ********************
            // ADD OBJECT HANDLERS BELOW
            // ********************

            return

                ErrorResponse.TryHandle(msg, Streams.ErrorSubject) ||
                SubscribeResponse.TryHandle(msg, Streams.SubscribeSubject) ||

                BookResponse.TryHandle(msg, Streams.BookSubject, "orderBookL2") ||
                TradeResponse.TryHandle(msg, Streams.TradesSubject) ||
                QuoteResponse.TryHandle(msg, Streams.QuoteSubject) ||
                BookResponse.TryHandle(msg, Streams.Book25Subject, "orderBookL2_25") ||
                LiquidationResponse.TryHandle(msg, Streams.LiquidationSubject) ||
                PositionResponse.TryHandle(msg, Streams.PositionSubject) ||
                MarginResponse.TryHandle(msg, Streams.MarginSubject) ||
                OrderResponse.TryHandle(msg, Streams.OrderSubject) ||
                WalletResponse.TryHandle(msg, Streams.WalletSubject) ||
                ExecutionResponse.TryHandle(msg, Streams.ExecutionSubject) ||
                FundingResponse.TryHandle(msg, Streams.FundingsSubject) ||
                InstrumentResponse.TryHandle(msg, Streams.InstrumentSubject) ||
                TradeBinResponse.TryHandle(msg, Streams.TradeBinSubject) ||


                InfoResponse.TryHandle(msg, Streams.InfoSubject) ||
                AuthenticationResponse.TryHandle(msg, Streams.AuthenticationSubject);
        }
    }
}
