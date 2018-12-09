using System;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Bitmex.Client.Websocket.Client;
using Bitmex.Client.Websocket.Communicator;
using Bitmex.Client.Websocket.Requests;
using Bitmex.Client.Websocket.Responses;
using Bitmex.Client.Websocket.Responses.Books;
using Bitmex.Client.Websocket.Responses.Trades;
using Bitmex.Client.Websocket.Websockets;
using Serilog;

namespace Bitmex.Client.Websocket.Sample.WinForms
{
    class StatsPresenter
    {
        private readonly IStatsView _view;

        private TradeStatsComputer _tradeStatsComputer;
        private OrderBookStatsComputer _orderBookStatsComputer;

        private IBitmexCommunicator _communicator;
        private BitmexWebsocketClient _client;

        private IDisposable _pingSubscription;
        private DateTime _pingRequest;

        private readonly string _selectedPair = "XBTUSD";

        public StatsPresenter(IStatsView view)
        {
            _view = view;

            HandleCommands();
        }

        private void HandleCommands()
        {
            _view.OnInit = OnInit;
            _view.OnStart = async () => await OnStart();
            _view.OnStop = OnStop;
        }

        private void OnInit()
        {
            Clear();
        }

        private async Task OnStart()
        {
            _tradeStatsComputer = new TradeStatsComputer();
            _orderBookStatsComputer = new OrderBookStatsComputer();

            var url = BitmexValues.ApiWebsocketUrl;
            _communicator = new BitmexWebsocketCommunicator(url);
            _client = new BitmexWebsocketClient(_communicator);

            Subscribe(_client);

            await _communicator.Start();
            await SendSubscriptions(_client);
            StartPingCheck(_client);
        }

        private void OnStop()
        {
            _pingSubscription.Dispose();
            _client.Dispose();
            _communicator.Dispose();
            _client = null;
            _communicator = null;
            Clear();
        }

        private void Subscribe(BitmexWebsocketClient client)
        {
            client.Streams.TradesStream.Subscribe(HandleTrades);
            client.Streams.BookStream.Subscribe(HandleOrderBook);
            client.Streams.PongStream.Subscribe(HandlePong);
        }

        private async Task SendSubscriptions(BitmexWebsocketClient client)
        {
            await client.Send(new TradesSubscribeRequest(_selectedPair));
            await client.Send(new BookSubscribeRequest(_selectedPair));
        }

        private void HandleTrades(TradeResponse response)
        {
            if (response.Action != BitmexAction.Insert && response.Action != BitmexAction.Partial)
                return;

            foreach (var trade in response.Data)
            {
                Log.Information($"Received [{trade.Side}] trade, price: {trade.Price}, amount: {trade.Size}");
                _tradeStatsComputer.HandleTrade(trade);
            }

            FormatTradesStats(_view.Trades1Min, _tradeStatsComputer.GetStatsFor(1));
            FormatTradesStats(_view.Trades5Min, _tradeStatsComputer.GetStatsFor(5));
            FormatTradesStats(_view.Trades15Min, _tradeStatsComputer.GetStatsFor(15));
        }

        private void FormatTradesStats(Action<string, Side> setAction, TradeStats trades)
        {
            if (trades == TradeStats.NULL)
                return;

            if (trades.BuysPerc >= trades.SellsPerc)
            {
                setAction($"{trades.BuysPerc:###}% buys", Side.Buy);
                return;
            }
            setAction($"{trades.SellsPerc:###}% sells", Side.Sell);
        }

        private void HandleOrderBook(BookResponse response)
        {
            _orderBookStatsComputer.HandleOrderBook(response);

            var stats = _orderBookStatsComputer.GetStats();
            if (stats == OrderBookStats.NULL)
                return;

            _view.Bid = stats.Bid.ToString("#.0");
            _view.Ask = stats.Ask.ToString("#.0");

            _view.BidAmount = stats.BidAmountPerc.ToString("###") + "%";
            _view.AskAmount = stats.AskAmountPerc.ToString("###") + "%";
        }

        private void StartPingCheck(BitmexWebsocketClient client)
        {
            _pingSubscription = Observable
                .Interval(TimeSpan.FromSeconds(5))
                .Subscribe(async x =>
                {
                    _pingRequest = DateTime.UtcNow;
                    await client.Send(new PingRequest());
                });      
        }

        private void HandlePong(PongResponse pong)
        {
            var current = DateTime.UtcNow;
            ComputePing(current, _pingRequest);
        }

        private void ComputePing(DateTime current, DateTime before)
        {
            var diff = current.Subtract(before);
            _view.Ping = $"{diff.TotalMilliseconds:###} ms";
        }

        private void Clear()
        {
            _view.Bid = string.Empty;
            _view.Ask = string.Empty;
            _view.BidAmount = string.Empty;
            _view.AskAmount = string.Empty;
            _view.Trades1Min(string.Empty, Side.Buy);
            _view.Trades5Min(string.Empty, Side.Buy);
            _view.Trades15Min(string.Empty, Side.Buy);
        }
    }
}
