using System;
using System.Reactive.Linq;
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

namespace Bitmex.Client.Websocket.Client
{
    /// <summary>
    /// All provided streams.
    /// You need to send subscription request in advance (via method `Send()` on BitmexWebsocketClient)
    /// </summary>
    public class BitmexClientStreams
    {
        internal readonly Subject<ErrorResponse> ErrorSubject = new Subject<ErrorResponse>();
        internal readonly Subject<InfoResponse> InfoSubject = new Subject<InfoResponse>();
        internal readonly Subject<PongResponse> PongSubject = new Subject<PongResponse>();
        internal readonly Subject<SubscribeResponse> SubscribeSubject = new Subject<SubscribeResponse>();
        internal readonly Subject<AuthenticationResponse> AuthenticationSubject = new Subject<AuthenticationResponse>();

        internal readonly Subject<TradeResponse> TradesSubject = new Subject<TradeResponse>();
        internal readonly Subject<TradeBinResponse> TradeBinSubject = new Subject<TradeBinResponse>();
        internal readonly Subject<BookResponse> BookSubject = new Subject<BookResponse>();
        internal readonly Subject<QuoteResponse> QuoteSubject = new Subject<QuoteResponse>();
        internal readonly Subject<LiquidationResponse> LiquidationSubject = new Subject<LiquidationResponse>();
        internal readonly Subject<InstrumentResponse> InstrumentSubject = new Subject<InstrumentResponse>();

        internal readonly Subject<WalletResponse> WalletSubject = new Subject<WalletResponse>();
        internal readonly Subject<OrderResponse> OrderSubject = new Subject<OrderResponse>();
        internal readonly Subject<PositionResponse> PositionSubject = new Subject<PositionResponse>();
        internal readonly Subject<MarginResponse> MarginSubject = new Subject<MarginResponse>();


        // PUBLIC

        /// <summary>
        /// Server errors stream
        /// </summary>
        public IObservable<ErrorResponse> ErrorStream => ErrorSubject.AsObservable();

        /// <summary>
        /// Info stream, sends initial information after reconnection
        /// </summary>
        public IObservable<InfoResponse> InfoStream => InfoSubject.AsObservable();

        /// <summary>
        /// Response stream to every ping request
        /// </summary>
        public IObservable<PongResponse> PongStream => PongSubject.AsObservable();

        /// <summary>
        /// Subscription info stream, emits status after sending subscription request
        /// </summary>
        public IObservable<SubscribeResponse> SubscribeStream => SubscribeSubject.AsObservable();

        /// <summary>
        /// Trades stream - emits every executed trade on Bitmex
        /// </summary>
        public IObservable<TradeResponse> TradesStream => TradesSubject.AsObservable();

        /// <summary>
        /// Chunk of trades - emits grouped trades together
        /// </summary>
        public IObservable<TradeBinResponse> TradeBinStream => TradeBinSubject.AsObservable();

        /// <summary>
        /// Order book stream - emits every update in the order book
        /// </summary>
        public IObservable<BookResponse> BookStream => BookSubject.AsObservable();

        /// <summary>
        /// Quotes stream - emits on every change of top level of order book
        /// </summary>
        public IObservable<QuoteResponse> QuoteStream => QuoteSubject.AsObservable();

        /// <summary>
        /// Liquidation stream - emits message whenever liquidation happens on Bitmex
        /// </summary>
        public IObservable<LiquidationResponse> LiquidationStream => LiquidationSubject.AsObservable();

        /// <summary>
        /// Stream of all Trade-able Contracts, Indices, and History
        /// </summary>
        public IObservable<InstrumentResponse> InstrumentStream => InstrumentSubject.AsObservable();



        // PRIVATE

        /// <summary>
        /// Authentication info stream, emits status after sending authentication request
        /// </summary>
        public IObservable<AuthenticationResponse> AuthenticationStream => AuthenticationSubject.AsObservable();

        /// <summary>
        /// Stream for every wallet balance update
        /// </summary>
        public IObservable<WalletResponse> WalletStream => WalletSubject.AsObservable();

        /// <summary>
        /// Stream of all your active orders
        /// </summary>
        public IObservable<OrderResponse> OrderStream => OrderSubject.AsObservable();

        /// <summary>
        /// Stream of all your active positions
        /// </summary>
        public IObservable<PositionResponse> PositionStream => PositionSubject.AsObservable();

        /// <summary>
        /// Stream of updates on your current account balance and margin requirements
        /// </summary>
        public IObservable<MarginResponse> MarginStream => MarginSubject.AsObservable();
    }
}
