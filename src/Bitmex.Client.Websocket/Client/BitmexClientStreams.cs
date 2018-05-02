using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Bitmex.Client.Websocket.Responses;
using Bitmex.Client.Websocket.Responses.Books;
using Bitmex.Client.Websocket.Responses.Orders;
using Bitmex.Client.Websocket.Responses.Trades;
using Bitmex.Client.Websocket.Responses.Wallets;

namespace Bitmex.Client.Websocket.Client
{
    public class BitmexClientStreams
    {
        internal readonly Subject<ErrorResponse> ErrorSubject = new Subject<ErrorResponse>();
        internal readonly Subject<InfoResponse> InfoSubject = new Subject<InfoResponse>();
        internal readonly Subject<PongResponse> PongSubject = new Subject<PongResponse>();
        internal readonly Subject<SubscribeResponse> SubscribeSubject = new Subject<SubscribeResponse>();
        internal readonly Subject<AuthenticationResponse> AuthenticationSubject = new Subject<AuthenticationResponse>();

        internal readonly Subject<TradeResponse> TradesSubject = new Subject<TradeResponse>();
        internal readonly Subject<BookResponse> BookSubject = new Subject<BookResponse>();

        internal readonly Subject<WalletResponse> WalletSubject = new Subject<WalletResponse>();
        internal readonly Subject<OrderResponse> OrderSubject = new Subject<OrderResponse>();



        public IObservable<ErrorResponse> ErrorStream => ErrorSubject.AsObservable();
        public IObservable<InfoResponse> InfoStream => InfoSubject.AsObservable();
        public IObservable<PongResponse> PongStream => PongSubject.AsObservable();
        public IObservable<SubscribeResponse> SubscribeStream => SubscribeSubject.AsObservable();
        public IObservable<AuthenticationResponse> AuthenticationStream => AuthenticationSubject.AsObservable();

        public IObservable<TradeResponse> TradesStream => TradesSubject.AsObservable();
        public IObservable<BookResponse> BookStream => BookSubject.AsObservable();

        /// <summary>
        /// Stream for every wallet balance update
        /// </summary>
        public IObservable<WalletResponse> WalletStream => WalletSubject.AsObservable();


        /// <summary>
        /// Stream of all active orders
        /// </summary>
        public IObservable<OrderResponse> OrderStream => OrderSubject.AsObservable();
    }
}
