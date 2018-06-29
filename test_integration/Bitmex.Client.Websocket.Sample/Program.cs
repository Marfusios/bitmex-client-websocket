using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Threading;
using Bitmex.Client.Websocket.Client;
using Bitmex.Client.Websocket.Requests;
using Bitmex.Client.Websocket.Websockets;
using Serilog;
using Serilog.Events;

namespace Bitmex.Client.Websocket.Sample
{
    class Program
    {
        private static readonly ManualResetEvent ExitEvent = new ManualResetEvent(false);

        private static readonly string API_KEY = "";
        private static readonly string API_SECRET = "";

        static void Main(string[] args)
        {
            InitLogging();

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
            using (var communicator = new BitmexWebsocketCommunicator(url))
            {
                using (var client = new BitmexWebsocketClient(communicator))
                {

                    client.Streams.InfoStream.Subscribe(info =>
                    {
                        Log.Information($"Reconnection happened, Message: {info.Info}, Version: {info.Version:D}");

                            
                        //client.Send(new PingRequest()).Wait();
                        //client.Send(new BookSubscribeRequest()).Wait();
                        client.Send(new TradesSubscribeRequest("XBTUSD")).Wait();
                        //client.Send(new QuoteSubscribeRequest("XBTUSD")).Wait();
                        client.Send(new LiquidationSubscribeRequest()).Wait();

                        if (!string.IsNullOrWhiteSpace(API_SECRET))
                            client.Send(new AuthenticationRequest(API_KEY, API_SECRET)).Wait();
                    });   

                    client.Streams.ErrorStream.Subscribe(x =>
                        Log.Warning($"Error received, message: {x.Error}, status: {x.Status}"));

                    client.Streams.AuthenticationStream.Subscribe(x =>
                    {
                        Log.Information($"Authentication happened, success: {x.Success}");
                        client.Send(new WalletSubscribeRequest()).Wait();
                        client.Send(new OrderSubscribeRequest()).Wait();
                        client.Send(new PositionSubscribeRequest()).Wait();
                    });


                    client.Streams.SubscribeStream.Subscribe(x =>
                        Log.Information($"Subscribed ({x.Success}) to {x.Subscribe}"));

                    client.Streams.PongStream.Subscribe(x =>
                        Log.Information($"Pong received ({x.Message})"));


                    client.Streams.WalletStream.Subscribe(y =>
                        y.Data.ToList().ForEach(x => 
                            Log.Information($"Wallet {x.Account}, {x.Currency} amount: {x.BalanceBtc}"))
                    );

                    client.Streams.OrderStream.Subscribe(y =>
                        y.Data.ToList().ForEach(x =>
                            Log.Information(
                                $"Order {x.Symbol} updated. Time: {x.Timestamp:HH:mm:ss.fff}, Amount: {x.OrderQty}, " +
                                $"Price: {x.Price}, Direction: {x.Side}, Working: {x.WorkingIndicator}, Status: {x.OrdStatus}"))
                    );

                    client.Streams.PositionStream.Subscribe(y =>
                        y.Data.ToList().ForEach(x =>
                            Log.Information(
                                $"Position {x.Symbol}, {x.Currency} updated. Time: {x.Timestamp:HH:mm:ss.fff}, Amount: {x.CurrentQty}, " +
                                $"Price: {x.LastPrice}, PNL: {x.UnrealisedPnl}"))
                    );

                    client.Streams.TradesStream.Subscribe(y =>
                       y.Data.ToList().ForEach(x => 
                           Log.Information($"Trade {x.Symbol} executed. Time: {x.Timestamp:mm:ss.fff}, Amount: {x.Size}, " +
                                           $"Price: {x.Price}, Direction: {x.TickDirection}"))
                        );

                    client.Streams.BookStream.Subscribe(book =>
                        book.Data.Take(100).ToList().ForEach(x => Log.Information(
                            $"Book | {book.Action} pair: {x.Symbol}, price: {x.Price}, amount {x.Size}, side: {x.Side}"))
                        );

                    client.Streams.QuoteStream.Subscribe(y =>
                        y.Data.ToList().ForEach(x => 
                            Log.Information($"Quote {x.Symbol}. Bid: {x.BidPrice} - {x.BidSize} Ask: {x.AskPrice} - {x.AskSize}"))
                    );

                    client.Streams.LiquidationStream.Subscribe(y =>
                        y.Data.ToList().ForEach(x =>
                            Log.Information($"Liquadation Action:{y.Action} OrderID:{x.OrderID} Symbol:{x.Symbol} Side:{x.Side} Price:{x.Price} leavesQty:{x.leavesQty}"))
                    );


                    communicator.Start();
                    
                    ExitEvent.WaitOne();
                }
            }

            Log.Debug("====================================");
            Log.Debug("              STOPPING              ");
            Log.Debug("====================================");
            Log.CloseAndFlush();
        }

        private static void InitLogging()
        {
            var executingDir = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            var logPath = Path.Combine(executingDir, "logs", "verbose.log");
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.File(logPath, rollingInterval: RollingInterval.Day)
                .WriteTo.ColoredConsole(LogEventLevel.Information)
                .CreateLogger();
        }

        private static void CurrentDomainOnProcessExit(object sender, EventArgs eventArgs)
        {
            Log.Warning("Exiting process");
            ExitEvent.Set();
        }

        private static void DefaultOnUnloading(AssemblyLoadContext assemblyLoadContext)
        {
            Log.Warning("Unloading process");
            ExitEvent.Set();
        }

        private static void ConsoleOnCancelKeyPress(object sender, ConsoleCancelEventArgs e)
        {
            Log.Warning("Canceling process");
            e.Cancel = true;
            ExitEvent.Set();
        }
    }
}
