using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Bitmex.Client.Websocket.Responses;
using Bitmex.Client.Websocket.Responses.Books;
using Bitmex.Client.Websocket.Responses.Orders;
using Bitmex.Client.Websocket.Responses.Tickers;
using Bitmex.Client.Websocket.Responses.Trades;
using Bitmex.Client.Websocket.Responses.Wallets;

namespace Bitmex.Client.Websocket.Client
{
    public class BitmexClientStreams
    {
        internal readonly Subject<ErrorResponse> ErrorSubject = new Subject<ErrorResponse>();
        internal readonly Subject<InfoResponse> InfoSubject = new Subject<InfoResponse>();
        internal readonly Subject<PongResponse> PongSubject = new Subject<PongResponse>();
        internal readonly Subject<AuthenticationResponse> AuthenticationSubject = new Subject<AuthenticationResponse>();
        internal readonly Subject<Ticker> TickerSubject = new Subject<Ticker>();
        internal readonly Subject<TradeResponse> TradesSubject = new Subject<TradeResponse>();
        internal readonly Subject<BookResponse> BookSubject = new Subject<BookResponse>();

        internal readonly Subject<Wallet[]> WalletsSubject = new Subject<Wallet[]>();
        internal readonly Subject<Wallet> WalletSubject = new Subject<Wallet>();
        internal readonly Subject<Order[]> OrdersSubject = new Subject<Order[]>();
        internal readonly Subject<Order> OrderCreatedSubject = new Subject<Order>();
        internal readonly Subject<Order> OrderUpdatedSubject = new Subject<Order>();
        internal readonly Subject<Order> OrderCanceledSubject = new Subject<Order>();



        public IObservable<ErrorResponse> ErrorStream => ErrorSubject.AsObservable();
        public IObservable<InfoResponse> InfoStream => InfoSubject.AsObservable();
        public IObservable<PongResponse> PongStream => PongSubject.AsObservable();
        public IObservable<AuthenticationResponse> AuthenticationStream => AuthenticationSubject.AsObservable();
        public IObservable<Ticker> TickerStream => TickerSubject.AsObservable();
        public IObservable<TradeResponse> TradesStream => TradesSubject.AsObservable();
        public IObservable<BookResponse> BookStream => BookSubject.AsObservable();

        /// <summary>
        /// Initial info about all wallets/balances (streamed only on authentication)
        /// </summary>
        public IObservable<Wallet[]> WalletsStream => WalletsSubject.AsObservable();

        /// <summary>
        /// Stream for every wallet balance update (initial wallets info is also streamed, same as 'WalletsStream')
        /// </summary>
        public IObservable<Wallet> WalletStream => WalletSubject.AsObservable();


        /// <summary>
        /// Initial info about all opened orders (streamed only on authentication)
        /// </summary>
        public IObservable<Order[]> OrdersStream => OrdersSubject.AsObservable();

        public IObservable<Order> OrderCreatedStream => OrderCreatedSubject.AsObservable();
        public IObservable<Order> OrderUpdatedStream => OrderUpdatedSubject.AsObservable();
        public IObservable<Order> OrderCanceledStream => OrderCanceledSubject.AsObservable();
    }
}
