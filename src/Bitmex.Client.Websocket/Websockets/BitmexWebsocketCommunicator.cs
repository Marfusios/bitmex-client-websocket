using System;
using System.Net.WebSockets;
using Bitmex.Client.Websocket.Communicator;
using Websocket.Client;

namespace Bitmex.Client.Websocket.Websockets
{
    /// <inheritdoc cref="WebsocketClient" />
    public class BitmexWebsocketCommunicator : WebsocketClient, IBitmexCommunicator
    {
        /// <inheritdoc />
        public BitmexWebsocketCommunicator(Uri url, Func<ClientWebSocket> clientFactory = null) 
            : base(url, clientFactory)
        {
        }
    }
}
