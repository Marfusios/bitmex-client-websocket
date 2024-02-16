using System;
using System.Net.WebSockets;
using Bitmex.Client.Websocket.Communicator;
using Microsoft.Extensions.Logging;
using Websocket.Client;

namespace Bitmex.Client.Websocket.Websockets
{
    /// <inheritdoc cref="WebsocketClient" />
    public class BitmexWebsocketCommunicator : WebsocketClient, IBitmexCommunicator
    {
        /// <inheritdoc />
        public BitmexWebsocketCommunicator(Uri url, Func<ClientWebSocket>? clientFactory = null)
            : base(url, clientFactory)
        {
        }

        /// <inheritdoc />
        public BitmexWebsocketCommunicator(Uri url, ILogger<BitmexWebsocketCommunicator> logger, Func<ClientWebSocket>? clientFactory = null)
            : base(url, logger, clientFactory)
        {
        }
    }
}
