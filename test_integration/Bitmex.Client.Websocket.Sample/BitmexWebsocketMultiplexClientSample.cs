using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Bitmex.Client.Websocket.Client;
using Bitmex.Client.Websocket.Messages;
using Bitmex.Client.Websocket.Requests;
using Bitmex.Client.Websocket.Websockets;
using Serilog;

namespace Bitmex.Client.Websocket.Sample
{
    class BitmexWebsocketMultiplexClientSample
    {
        private static readonly string API_KEY = "your api key";
        private static readonly string API_SECRET = "";

        public static void RunSample(ManualResetEvent ExitEvent)
        {
            Console.WriteLine("|=================================|");
            Console.WriteLine("|     BITMEX MULTIPLEX CLIENT     |");
            Console.WriteLine("|=================================|");
            Console.WriteLine();

            Log.Debug("====================================");
            Log.Debug("              STARTING              ");
            Log.Debug("====================================");
           


            var url = BitmexValues.ApiMultiplexingWebsocketUrl;
            using (var communicator = new BitmexWebsocketCommunicator(url))
            {
                communicator.Name = "Bitmex-Multiplex-1";
                communicator.ReconnectTimeoutMs = (int)TimeSpan.FromMinutes(10).TotalMilliseconds;
                communicator.ReconnectionHappened.Subscribe(type =>
                    Log.Information($"Reconnection happened, type: {type}"));

                using (var client = new BitmexMultiplexClient(communicator))
                {
                    client.Channels.Subscribe(channel =>
                        channel.Streams.InfoStream.Subscribe(info =>
                    {
                        Log.Information($"Channel = {channel.ChannelName} | Reconnection happened, Message: {info.Info}, Version: {info.Version:D}");
                    }));

                    client.Channels.Subscribe(channel => channel.Streams.TradesStream.Subscribe(y =>
                        y.Data.ToList().ForEach(x =>
                            Log.Information(
                                $"Trade {x.Symbol} executed. Time: {x.Timestamp:mm:ss.fff}, [{x.Side}] Amount: {x.Size}, " +
                                $"Price: {x.Price}"))
                    ));

                    //List<BitmexWebsocketChannel> channels = CreateChannels(client).Result;
                    var channels = new List<BitmexWebsocketChannel>
                    {
                        client.CreateChannel("test").Result
                    };

                    SubscribeToStreams(client);

                    SendSubscriptionRequests(client, channels);

                    communicator.Start();

                    ExitEvent.WaitOne();
                }
            }

            Log.Debug("====================================");
            Log.Debug("              STOPPING              ");
            Log.Debug("====================================");
            Log.CloseAndFlush();
        }

        private static async Task<List<BitmexWebsocketChannel>> CreateChannels(BitmexMultiplexClient client)
        {
            return new List<BitmexWebsocketChannel>
            {
                await client.CreateChannel("TestChannel1"),
                await client.CreateChannel("TestChannel2")
            };
        }

        private static async Task SendSubscriptionRequests(BitmexMultiplexClient client, List<BitmexWebsocketChannel> channels)
        {
            foreach (var channel in channels)
            {
                await client.Send(channel, new MultiplexingMessageBase
                    {
                        MessageType = MultiplexingMessageType.Message,
                        Payload = new PingRequest()
                    }
                );

                await client.Send(channel, new MultiplexingMessageBase
                    {
                        MessageType = MultiplexingMessageType.Message,
                        Payload = new TradesSubscribeRequest("XBTUSD")
                    }
                );

                await client.Send(channel, new MultiplexingMessageBase
                    {
                        MessageType = MultiplexingMessageType.Message,
                        Payload = new QuoteSubscribeRequest("XBTUSD")
                    }
                );

                //if (!string.IsNullOrWhiteSpace(API_SECRET))
                //    await client.Send(channel, new MultiplexingMessageBase
                //        {
                //            MessageType = MultiplexingMessageType.Message,
                //            Payload = new AuthenticationRequest(API_KEY, API_SECRET)
                //        }
                //    );
            }
        }

        private static void SubscribeToStreams(BitmexMultiplexClient client)
        {
            client.Channels.Subscribe(channel =>
                channel.Streams.ErrorStream.Subscribe(x =>
                Log.Warning($"Error received, message: {x.Error}, status: {x.Status}")));

            client.Channels.Subscribe(channel =>
                channel.Streams.AuthenticationStream.Subscribe(x =>
                {
                    Log.Information($"Authentication happened, success: {x.Success}");
                    client.Send(channel, new MultiplexingMessageBase
                        {
                            MessageType = MultiplexingMessageType.Message,
                            Payload = new WalletSubscribeRequest()
                        }
                    ).Wait();
                    client.Send(channel, new MultiplexingMessageBase
                        {
                            MessageType = MultiplexingMessageType.Message,
                            Payload = new OrderSubscribeRequest()
                        }
                    ).Wait();
                    client.Send(channel, new MultiplexingMessageBase
                        {
                            MessageType = MultiplexingMessageType.Message,
                            Payload = new PositionSubscribeRequest()
                        }
                    ).Wait();
                }));


            client.Channels.Subscribe(channel =>
                channel.Streams.SubscribeStream.Subscribe(x =>
            {
                Log.Information(x.IsSubscription
                    ? $"Subscribed ({x.Success}) to {x.Subscribe}"
                    : $"Unsubscribed ({x.Success}) from {x.Unsubscribe}");
            }));

            client.Channels.Subscribe(channel =>
                channel.Streams.PongStream.Subscribe(x =>
                    Log.Information($"Pong received ({x.Message})")));


            client.Channels.Subscribe(channel =>
                channel.Streams.WalletStream.Subscribe(y =>
                y.Data.ToList().ForEach(x =>
                    Log.Information($"Wallet {x.Account}, {x.Currency} amount: {x.BalanceBtc}"))
            ));

            client.Channels.Subscribe(channel =>
                channel.Streams.OrderStream.Subscribe(y =>
                    y.Data.ToList().ForEach(x =>
                        Log.Information(
                            $"Order {x.Symbol} updated. Time: {x.Timestamp:HH:mm:ss.fff}, Amount: {x.OrderQty}, " +
                            $"Price: {x.Price}, Direction: {x.Side}, Working: {x.WorkingIndicator}, Status: {x.OrdStatus}"))
                ));

            client.Channels.Subscribe(channel =>
                channel.Streams.PositionStream.Subscribe(y =>
                    y.Data.ToList().ForEach(x =>
                        Log.Information(
                            $"Position {x.Symbol}, {x.Currency} updated. Time: {x.Timestamp:HH:mm:ss.fff}, Amount: {x.CurrentQty}, " +
                            $"Price: {x.LastPrice}, PNL: {x.UnrealisedPnl}"))
                ));

            client.Channels.Subscribe(channel =>
                channel.Streams.TradesStream.Subscribe(y =>
                    y.Data.ToList().ForEach(x =>
                        Log.Information(
                            $"Trade {x.Symbol} executed. Time: {x.Timestamp:mm:ss.fff}, [{x.Side}] Amount: {x.Size}, " +
                            $"Price: {x.Price}"))
                ));

            client.Channels.Subscribe(channel =>
                channel.Streams.BookStream.Subscribe(book =>
                    book.Data.Take(100).ToList().ForEach(x => Log.Information(
                        $"Book | {book.Action} pair: {x.Symbol}, price: {x.Price}, amount {x.Size}, side: {x.Side}"))
                ));

            client.Channels.Subscribe(channel =>
                channel.Streams.QuoteStream.Subscribe(y =>
                    y.Data.ToList().ForEach(x =>
                        Log.Information(
                            $"Quote {x.Symbol}. Bid: {x.BidPrice} - {x.BidSize} Ask: {x.AskPrice} - {x.AskSize}"))
                ));

            client.Channels.Subscribe(channel =>
                channel.Streams.LiquidationStream.Subscribe(y =>
                    y.Data.ToList().ForEach(x =>
                        Log.Information(
                            $"Liquadation Action: {y.Action}, OrderID: {x.OrderID}, Symbol: {x.Symbol}, Side: {x.Side}, Price: {x.Price}, LeavesQty: {x.leavesQty}"))
                ));

            client.Channels.Subscribe(channel =>
                channel.Streams.TradeBinStream.Subscribe(y =>
                    y.Data.ToList().ForEach(x =>
                        Log.Information(
                            $"TradeBin table:{y.Table} {x.Symbol} executed. Time: {x.Timestamp:mm:ss.fff}, Open: {x.Open}, " +
                            $"Close: {x.Close}, Volume: {x.Volume}, Trades: {x.Trades}"))
                ));

            client.Channels.Subscribe(channel =>
                channel.Streams.InstrumentStream.Subscribe(x =>
                {
                    x.Data.ToList().ForEach(y =>
                    {
                        Log.Information($"Instrument, {y.Symbol}, " +
                                        $"price: {y.MarkPrice}, last: {y.LastPrice}, " +
                                        $"mark: {y.MarkMethod}, fair: {y.FairMethod}, direction: {y.LastTickDirection}, " +
                                        $"funding: {y.FundingRate} i: {y.IndicativeFundingRate} s: {y.FundingQuoteSymbol}");
                    });
                }));


            // example of unsubscribe requests
            //Task.Run(async () =>
            //{
            //    await Task.Delay(5000);
            //    await client.Send(new BookSubscribeRequest("XBTUSD") {IsUnsubscribe = true});
            //    await Task.Delay(5000);
            //    await client.Send(new TradesSubscribeRequest() {IsUnsubscribe = true});
            //});
        }
    }
}
