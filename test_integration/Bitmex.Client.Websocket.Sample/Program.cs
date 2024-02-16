﻿using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Threading;
using System.Threading.Tasks;
using Bitmex.Client.Websocket.Client;
using Bitmex.Client.Websocket.Requests;
using Bitmex.Client.Websocket.Websockets;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Extensions.Logging;

namespace Bitmex.Client.Websocket.Sample
{
    class Program
    {
        private static readonly ManualResetEvent _exitEvent = new(false);

        private const string ApiKey = "your_api_key";
        private const string ApiSecret = "";

        static void Main(string[] args)
        {
            var logger = InitLogging();

            AppDomain.CurrentDomain.ProcessExit += CurrentDomainOnProcessExit;
            AssemblyLoadContext.Default.Unloading += DefaultOnUnloading;
            Console.CancelKeyPress += ConsoleOnCancelKeyPress;

            Console.WriteLine("|=======================|");
            Console.WriteLine("|     BITMEX CLIENT     |");
            Console.WriteLine("|=======================|");
            Console.WriteLine();

            Log.Debug("====================================");
            Log.Debug("              STARTING              ");
            Log.Debug("====================================");



            var url = BitmexValues.ApiWebsocketUrl;
            using (var communicator = new BitmexWebsocketCommunicator(url, logger.CreateLogger<BitmexWebsocketCommunicator>()))
            {
                communicator.Name = "Bitmex-1";
                communicator.ReconnectTimeout = TimeSpan.FromMinutes(10);
                communicator.ReconnectionHappened.Subscribe(type =>
                    Log.Information("Reconnection happened, type: {type}", type.Type));

                using (var client = new BitmexWebsocketClient(communicator, logger.CreateLogger<BitmexWebsocketClient>()))
                {

                    client.Streams.InfoStream.Subscribe(info =>
                    {
                        Log.Information("Reconnection happened, Message: {info}, Version: {version}", info.Info, info.Version);
                        SendSubscriptionRequests(client);
                    });

                    SubscribeToStreams(client);

                    communicator.Start();

                    _ = StartPinging(client);

                    _exitEvent.WaitOne();
                }
            }

            Log.Debug("====================================");
            Log.Debug("              STOPPING              ");
            Log.Debug("====================================");
            Log.CloseAndFlush();
        }

        private static async Task StartPinging(BitmexWebsocketClient client)
        {
            await Task.Delay(TimeSpan.FromSeconds(30));
            client.Send(new PingRequest());
        }

        private static void SendSubscriptionRequests(BitmexWebsocketClient client)
        {
            client.Send(new PingRequest());
            //client.Send(new BookSubscribeRequest("XBTUSD"));
            client.Send(new TradesSubscribeRequest("XBTUSD"));
            //client.Send(new TradeBinSubscribeRequest("1m", "XBTUSD"));
            //client.Send(new TradeBinSubscribeRequest("5m", "XBTUSD"));
            client.Send(new QuoteSubscribeRequest("XBTUSD"));
            client.Send(new LiquidationSubscribeRequest());
            client.Send(new InstrumentSubscribeRequest("XBTUSD"));
            client.Send(new FundingsSubscribeRequest("XBTUSD"));
            //client.Send(new Book25SubscribeRequest("XBTUSD"));
            //client.Send(new Book10SubscribeRequest("XBTUSD"));

            if (!string.IsNullOrWhiteSpace(ApiSecret))
                client.Send(new AuthenticationRequest(ApiKey, ApiSecret));
        }

        private static void SubscribeToStreams(BitmexWebsocketClient client)
        {
            client.Streams.ErrorStream.Subscribe(x =>
                Log.Warning("Error received, message: {error}, status: {status}", x.Error, x.Status));

            client.Streams.AuthenticationStream.Subscribe(x =>
            {
                Log.Information($"Authentication happened, success: {x.Success}");
                client.Send(new WalletSubscribeRequest());
                client.Send(new OrderSubscribeRequest());
                client.Send(new PositionSubscribeRequest());
            });


            client.Streams.SubscribeStream.Subscribe(x =>
            {
                Log.Information(x.IsSubscription
                    ? $"Subscribed ({x.Success}) to {x.Subscribe}"
                    : $"Unsubscribed ({x.Success}) from {x.Unsubscribe}");
            });

            client.Streams.PongStream.Subscribe(x =>
                Log.Information($"Pong received ({x.Message})"));


            client.Streams.WalletStream.Subscribe(y =>
                y.Data.ToList().ForEach(x =>
                    Log.Information($"Wallet {x.Account}, {x.Currency} amount: {x.BalanceBtc}"))
            );

            client.Streams.OrderStream.Subscribe(y =>
                y.Data.ToList().ForEach(x =>
                    Log.Information(
                        $"Order {x.Symbol} {x.OrderId} updated. Time: {x.Timestamp:HH:mm:ss.fff}, Amount: {x.OrderQty}, " +
                        $"Price: {x.Price}, Direction: {x.Side}, Working: {x.WorkingIndicator}, Status: {x.OrdStatus}"))
            );

            client.Streams.PositionStream.Subscribe(y =>
                y.Data.ToList().ForEach(x =>
                    Log.Information(
                        $"Position {x.Symbol}, {x.Currency} updated. Time: {x.Timestamp:HH:mm:ss.fff}, Amount: {x.CurrentQty}, " +
                        $"Entry: {x.AvgEntryPrice}, Price: {x.LastPrice}, Liq: {x.LiquidationPrice}, " +
                        $"PNL: {x.UnrealisedPnl}"))
            );

            client.Streams.TradesStream.Subscribe(y =>
                y.Data.ToList().ForEach(x =>
                    Log.Information($"Trade {x.Symbol} executed. Time: {x.Timestamp:HH:mm:ss.fff}, [{x.Side}] Amount: {x.Size}, " +
                                    $"Price: {x.Price}, Match: {x.TrdMatchId}"))
            );

            client.Streams.BookStream.Subscribe(book =>
                book.Data.Take(100).ToList().ForEach(x => Log.Information(
                    $"Book | {book.Action} pair: {x.Symbol}, price: {x.Price}, amount {x.Size}, side: {x.Side}"))
            );

            client.Streams.Book25Stream.Subscribe(book =>
                book.Data.Take(100).ToList().ForEach(x => Log.Information(
                    $"Book | {book.Action} pair: {x.Symbol}, price: {x.Price}, amount {x.Size}, side: {x.Side}"))
            );

            client.Streams.QuoteStream.Subscribe(y =>
                y.Data.ToList().ForEach(x =>
                    Log.Information($"Quote {x.Symbol}. Bid: {x.BidPrice} - {x.BidSize} Ask: {x.AskPrice} - {x.AskSize}"))
            );

            client.Streams.LiquidationStream.Subscribe(y =>
                y.Data.ToList().ForEach(x =>
                    Log.Information(
                        $"Liquidation Action: {y.Action}, OrderID: {x.OrderID}, Symbol: {x.Symbol}, Side: {x.Side}, Price: {x.Price}, LeavesQty: {x.leavesQty}"))
            );

            client.Streams.TradeBinStream.Subscribe(y =>
                y.Data.ToList().ForEach(x =>
                Log.Information($"TradeBin table:{y.Table} {x.Symbol} executed. Time: {x.Timestamp:mm:ss.fff}, Open: {x.Open}, " +
                        $"Close: {x.Close}, Volume: {x.Volume}, Trades: {x.Trades}"))
            );

            client.Streams.InstrumentStream.Subscribe(x =>
            {
                x.Data.ToList().ForEach(y =>
                {
                    Log.Information($"Instrument, {y.Symbol}, " +
                                    $"price: {y.MarkPrice}, last: {y.LastPrice}, " +
                                    $"mark: {y.MarkMethod}, fair: {y.FairMethod}, direction: {y.LastTickDirection}, " +
                                    $"funding: {y.FundingRate} i: {y.IndicativeFundingRate} s: {y.FundingQuoteSymbol}");
                });
            });

            client.Streams.FundingStream.Subscribe(x =>
            {
                x.Data.ToList().ForEach(f =>
                {
                    Log.Information($"Funding {f.Symbol}, Timestamp: {f.Timestamp}, Interval: {f.FundingInterval}, " +
                                    $"Rate: {f.FundingRate}, RateDaily: {f.FundingRateDaily}");
                });
            });


            // example of unsubscribe requests
            //Task.Run(async () =>
            //{
            //    await Task.Delay(5000);
            //    client.Send(new BookSubscribeRequest("XBTUSD") { IsUnsubscribe = true });
            //    await Task.Delay(5000);
            //    client.Send(new TradesSubscribeRequest() { IsUnsubscribe = true });
            //});
        }

        private static SerilogLoggerFactory InitLogging()
        {
            var executingDir = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            var logPath = Path.Combine(executingDir, "logs", "verbose.log");
            var logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.File(logPath, rollingInterval: RollingInterval.Day)
                .WriteTo.Console(LogEventLevel.Information)
                .CreateLogger();
            Log.Logger = logger;
            return new SerilogLoggerFactory(logger);
        }

        private static void CurrentDomainOnProcessExit(object sender, EventArgs eventArgs)
        {
            Log.Warning("Exiting process");
            _exitEvent.Set();
        }

        private static void DefaultOnUnloading(AssemblyLoadContext assemblyLoadContext)
        {
            Log.Warning("Unloading process");
            _exitEvent.Set();
        }

        private static void ConsoleOnCancelKeyPress(object sender, ConsoleCancelEventArgs e)
        {
            Log.Warning("Canceling process");
            e.Cancel = true;
            _exitEvent.Set();
        }
    }
}
