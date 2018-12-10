using System;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Bitmex.Client.Websocket.Client;
using Bitmex.Client.Websocket.Communicator;
using Bitmex.Client.Websocket.Requests;
using Bitmex.Client.Websocket.Responses;
using Bitmex.Client.Websocket.Responses.Books;
using Bitmex.Client.Websocket.Responses.Trades;
using Bitmex.Client.Websocket.Sample.WinForms.Statistics;
using Bitmex.Client.Websocket.Sample.WinForms.Views;
using Bitmex.Client.Websocket.Websockets;
using Serilog;
using Websocket.Client;

namespace Bitmex.Client.Websocket.Sample.WinForms.Presenters
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

        private string _defaultPair = "XBTUSD";
        private string _currency = "$";

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
            var pair = _view.Pair;
            if (string.IsNullOrWhiteSpace(pair))
                pair = _defaultPair;
            pair = pair.ToUpper();

            _tradeStatsComputer = new TradeStatsComputer();
            _orderBookStatsComputer = new OrderBookStatsComputer();

            var url = BitmexValues.ApiWebsocketUrl;
            _communicator = new BitmexWebsocketCommunicator(url);
            _client = new BitmexWebsocketClient(_communicator);

            Subscribe(_client);

            _communicator.ReconnectionHappened.Subscribe(async type =>
            {
                _view.Status($"Reconnected (type: {type})", StatusType.Info);
                await SendSubscriptions(_client, pair);
            });

            _communicator.DisconnectionHappened.Subscribe(type =>
            {
                if (type == DisconnectionType.Error)
                {
                    _view.Status($"Disconnected by error, next try in {_communicator.ErrorReconnectTimeoutMs/1000} sec", 
                        StatusType.Error);
                    return;
                }
                _view.Status($"Disconnected (type: {type})", 
                    StatusType.Warning);
            });

            await _communicator.Start();

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
            client.Streams.TradesStream.ObserveOn(TaskPoolScheduler.Default).Subscribe(HandleTrades);
            client.Streams.BookStream.ObserveOn(TaskPoolScheduler.Default).Subscribe(HandleOrderBook);
            client.Streams.PongStream.ObserveOn(TaskPoolScheduler.Default).Subscribe(HandlePong);
        }

        private async Task SendSubscriptions(BitmexWebsocketClient client, string pair)
        {
            await client.Send(new TradesSubscribeRequest(pair));
            await client.Send(new BookSubscribeRequest(pair));
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
            FormatTradesStats(_view.Trades1Hour, _tradeStatsComputer.GetStatsFor(60));
            FormatTradesStats(_view.Trades24Hours, _tradeStatsComputer.GetStatsFor(60 * 24));
        }

        private void FormatTradesStats(Action<string, Side> setAction, TradeStats trades)
        {
            if (trades == TradeStats.NULL)
                return;

            if (trades.BuysPerc >= trades.SellsPerc)
            {
                setAction($"{trades.BuysPerc:###}% buys{Environment.NewLine}{trades.TotalCount}", Side.Buy);
                return;
            }
            setAction($"{trades.SellsPerc:###}% sells{Environment.NewLine}{trades.TotalCount}", Side.Sell);
        }

        private void HandleOrderBook(BookResponse response)
        {
            _orderBookStatsComputer.HandleOrderBook(response);

            var stats = _orderBookStatsComputer.GetStats();
            if (stats == OrderBookStats.NULL)
                return;

            _view.Bid = stats.Bid.ToString("#.0");
            _view.Ask = stats.Ask.ToString("#.0");

            _view.BidAmount = $"{stats.BidAmountPerc:###}%{Environment.NewLine}{FormatToMilions(stats.BidAmount)}";
            _view.AskAmount = $"{stats.AskAmountPerc:###}%{Environment.NewLine}{FormatToMilions(stats.AskAmount)}";
        }

        private string FormatToMilions(double amount)
        {
            var milions = amount / 1000000;
            return $"{_currency}{milions:#.00} M";
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
            _view.Status("Connected", StatusType.Info);
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
            _view.Trades1Hour(string.Empty, Side.Buy);
            _view.Trades24Hours(string.Empty, Side.Buy);
        }
    }
}
