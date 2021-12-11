using System;
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
using Bitmex.Client.Websocket.Responses.Instruments;
using Bitmex.Client.Websocket.Responses.Margins;
using Websocket.Client;
using Bitmex.Client.Websocket.Responses.Executions;
using Bitmex.Client.Websocket.Responses.Fundings;
using Microsoft.Extensions.Logging;
using Utf8Json;
using Utf8Json.Resolvers;

namespace Bitmex.Client.Websocket.Client;

/// <summary>
/// Bitmex websocket client.
/// Use method `Send()` to subscribe to channels.
/// And `Streams` to subscribe.
/// </summary>
public class BitmexWebsocketClient : IBitmexWebsocketClient
{
    readonly ILogger _logger;
    readonly IWebsocketClient _client;
    readonly IDisposable _messageReceivedSubscription;

    /// <summary>
    /// Creates a new instance.
    /// </summary>
    /// <param name="logger">The logger to use for logging any warnings or errors.</param>
    /// <param name="client">The client to use for the trade websocket.</param>
    public BitmexWebsocketClient(ILogger logger, IWebsocketClient client)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _client = client ?? throw new ArgumentNullException(nameof(client));
        _messageReceivedSubscription = _client.MessageReceived.Subscribe(HandleMessage);

        JsonSerializer.SetDefaultResolver(StandardResolver.CamelCase);
    }

    /// <inheritdoc />
    public BitmexClientStreams Streams { get; } = new();

    /// <summary>
    /// Cleanup everything.
    /// </summary>
    public void Dispose() => _messageReceivedSubscription?.Dispose();

    /// <inheritdoc />
    public void Send<T>(T request) where T : RequestBase
    {
        if (request == null) throw new ArgumentNullException(nameof(request));

        try
        {
            var serialized = request.IsRaw ?
                request.OperationString :
                BitmexJsonSerializer.Serialize(request);
            _client.Send(serialized);
        }
        catch (Exception e)
        {
            _logger.LogError(e, LogMessage($"Exception while sending message '{request}'. Error: {e.Message}"));
            throw;
        }
    }

    static string LogMessage(string message) => $"[BMX WEBSOCKET CLIENT] {message}";

    void HandleMessage(ResponseMessage message)
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
                Streams.UnhandledMessageStream.OnNext(messageSafe);

            _logger.LogWarning(LogMessage($"Unhandled response:  '{messageSafe}'"));
        }
        catch (Exception e)
        {
            _logger.LogError(e, LogMessage("Exception while receiving message"));
        }
    }

    bool HandleRawMessage(string message) => PongResponse.TryHandle(message, Streams.PongStream);

    bool HandleObjectMessage(string message)
    {
        return
            ErrorResponse.TryHandle(message, Streams.ErrorStream) ||
            SubscribeResponse.TryHandle(message, Streams.SubscribeStream) ||

            BookResponse.TryHandle(message, Streams.BookStream, "orderBookL2") ||
            TradeResponse.TryHandle(message, Streams.TradesStream) ||
            QuoteResponse.TryHandle(message, Streams.QuoteStream) ||
            BookResponse.TryHandle(message, Streams.Book25Stream, "orderBookL2_25") ||
            LiquidationResponse.TryHandle(message, Streams.LiquidationStream) ||
            PositionResponse.TryHandle(message, Streams.PositionStream) ||
            MarginResponse.TryHandle(message, Streams.MarginStream) ||
            OrderResponse.TryHandle(message, Streams.OrderStream) ||
            WalletResponse.TryHandle(message, Streams.WalletStream) ||
            ExecutionResponse.TryHandle(message, Streams.ExecutionStream) ||
            FundingResponse.TryHandle(message, Streams.FundingStream) ||
            InstrumentResponse.TryHandle(message, Streams.InstrumentStream) ||
            TradeBinResponse.TryHandle(message, Streams.TradeBinStream) ||

            InfoResponse.TryHandle(message, Streams.InfoStream) ||
            AuthenticationResponse.TryHandle(message, Streams.AuthenticationStream);
    }
}