using System;

namespace Bitmex.Client.Websocket
{
    public static class BitmexValues
    {
        public static readonly Uri ApiWebsocketUrl = new Uri("wss://www.bitmex.com/realtime");
        public static readonly Uri ApiWebsocketTestnetUrl = new Uri("wss://testnet.bitmex.com/realtime");
    }
}
