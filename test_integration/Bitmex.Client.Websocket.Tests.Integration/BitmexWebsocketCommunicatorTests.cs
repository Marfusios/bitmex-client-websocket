using System;
using System.Threading;
using System.Threading.Tasks;
using Bitmex.Client.Websocket.Communicator;
using Bitmex.Client.Websocket.Websockets;
using Xunit;

namespace Bitmex.Client.Websocket.Tests.Integration
{
    public class BitmexWebsocketCommunicatorTests
    {
        [Fact]
        public async Task OnStarting_ShouldGetInfoResponse()
        {
            var url = BitmexValues.ApiWebsocketUrl;
            using (var communicator = new BitmexWebsocketCommunicator(url))
            {
                string received = null;
                var receivedEvent = new ManualResetEvent(false);

                communicator.MessageReceived.Subscribe(msg =>
                {
                    received = msg;
                    receivedEvent.Set();
                });

                await communicator.Start();

                receivedEvent.WaitOne(TimeSpan.FromSeconds(30));

                Assert.NotNull(received);
            }
        }
    }
}
