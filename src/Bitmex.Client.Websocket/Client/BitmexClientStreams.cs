using System.Reactive.Subjects;
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
using Bitmex.Client.Websocket.Responses.Executions;
using Bitmex.Client.Websocket.Responses.Fundings;

namespace Bitmex.Client.Websocket.Client;

/// <summary>
/// All provided streams.
/// You need to send subscription request in advance (via method `Send()` on BitmexWebsocketClient)
/// </summary>
public class BitmexClientStreams
{
    // PUBLIC

    /// <summary>
    /// Server errors stream
    /// </summary>
    public readonly Subject<ErrorResponse> ErrorStream = new();

    /// <summary>
    /// Info stream, sends initial information after reconnection
    /// </summary>
    public readonly Subject<InfoResponse> InfoStream = new();

    /// <summary>
    /// Response stream to every ping request
    /// </summary>
    public readonly Subject<PongResponse> PongStream = new();

    /// <summary>
    /// Subscription info stream, emits status after sending subscription request
    /// </summary>
    public readonly Subject<SubscribeResponse> SubscribeStream = new();

    /// <summary>
    /// Trades stream - emits every executed trade on Bitmex
    /// </summary>
    public readonly Subject<TradeResponse> TradesStream = new();

    /// <summary>
    /// Chunk of trades - emits grouped trades together
    /// </summary>
    public readonly Subject<TradeBinResponse> TradeBinStream = new();

    /// <summary>
    /// Order book stream - emits every update in the order book
    /// </summary>
    public readonly Subject<BookResponse> BookStream = new();

    /// <summary>
    /// Order book stream - emits every update in the order book
    /// </summary>
    public readonly Subject<BookResponse> Book25Stream = new();

    /// <summary>
    /// Quotes stream - emits on every change of top level of order book
    /// </summary>
    public readonly Subject<QuoteResponse> QuoteStream = new();

    /// <summary>
    /// Liquidation stream - emits message whenever liquidation happens on Bitmex
    /// </summary>
    public readonly Subject<LiquidationResponse> LiquidationStream = new();

    /// <summary>
    /// Stream of all Trade-able Contracts, Indices, and History
    /// </summary>
    public readonly Subject<InstrumentResponse> InstrumentStream = new();

    /// <summary>
    /// Fundings stream - updates of swap funding rates. Sent every funding interval (usually 8hrs) 
    /// <para>!!! Any time you connect to the stream, you receive the latest active funding rate</para>
    /// </summary>
    public readonly Subject<FundingResponse> FundingStream = new();
        
    // PRIVATE

    /// <summary>
    /// Authentication info stream, emits status after sending authentication request
    /// </summary>
    public readonly Subject<AuthenticationResponse> AuthenticationStream = new();

    /// <summary>
    /// Stream for every wallet balance update
    /// </summary>
    public readonly Subject<WalletResponse> WalletStream = new();

    /// <summary>
    /// Stream of all your active orders
    /// </summary>
    public readonly Subject<OrderResponse> OrderStream = new();

    /// <summary>
    /// Stream of all your active positions
    /// </summary>
    public readonly Subject<PositionResponse> PositionStream = new();

    /// <summary>
    /// Stream of updates on your current account balance and margin requirements
    /// </summary>
    public readonly Subject<MarginResponse> MarginStream = new();

    /// <summary>
    /// Stream of all raw transactions, which includes order opening and cancellation, and order status changes
    /// </summary>
    public readonly Subject<ExecutionResponse> ExecutionStream = new();

    /// <summary>
    /// Stream of all raw unhandled messages (that are not yet implemented)
    /// </summary>
    public readonly Subject<string> UnhandledMessageStream = new();
}