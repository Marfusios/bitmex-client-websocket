using System;
using System.Threading;
using System.Threading.Tasks;
using Bitmex.Client.Websocket.Client;
using Bitmex.Client.Websocket.Requests;
using Bitmex.Client.Websocket.Responses;
using Bitmex.Client.Websocket.Websockets;
using Xunit;

namespace Bitmex.Client.Websocket.Tests.Integration
{
    public class BitmexWebsocketClientTests
    {
        private static readonly string API_KEY = "your_api_key";
        private static readonly string API_SECRET = "";

        [Fact]
        public async Task PingPong()
        {
            var url = BitmexValues.ApiWebsocketUrl;
            using (var communicator = new BitmexWebsocketCommunicator(url))
            {
                PongResponse received = null;
                var receivedEvent = new ManualResetEvent(false);

                using (var client = new BitmexWebsocketClient(communicator))
                {

                    client.Streams.PongStream.Subscribe(pong =>
                    {
                        received = pong;
                        receivedEvent.Set();
                    });

                    await communicator.Start();

                    await client.Send(new PingRequest() {Cid = 123456});

                    receivedEvent.WaitOne(TimeSpan.FromSeconds(30));

                    Assert.NotNull(received);
                    Assert.Equal(123456, received.Cid);
                    Assert.True(DateTime.UtcNow.Subtract(received.Ts).TotalSeconds < 15);
                }
            }
        }

        [SkippableFact]
        public async Task Authentication()
        {
            Skip.If(string.IsNullOrWhiteSpace(API_SECRET));

            var url = BitmexValues.ApiWebsocketUrl;
            using (var communicator = new BitmexWebsocketCommunicator(url))
            {
                AuthenticationResponse received = null;
                var receivedEvent = new ManualResetEvent(false);

                using (var client = new BitmexWebsocketClient(communicator))
                {

                    client.Streams.AuthenticationStream.Subscribe(auth =>
                    {
                        received = auth;
                        receivedEvent.Set();
                    });

                    await communicator.Start();

                    await client.Authenticate(API_KEY, API_SECRET);

                    receivedEvent.WaitOne(TimeSpan.FromSeconds(30));

                    Assert.NotNull(received);
                    Assert.True(received.IsAuthenticated);
                }
            }
        }

    }
}
