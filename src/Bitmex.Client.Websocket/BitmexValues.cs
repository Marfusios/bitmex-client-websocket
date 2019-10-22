using System;

namespace Bitmex.Client.Websocket
{
    /// <summary>
    /// Bitmex static urls
    /// </summary>
    public static class BitmexValues
    {
        /// <summary>
        /// Main Bitmex url to multiplexed-websocket API
        /// </summary>
        public static readonly Uri ApiMultiplexingWebsocketUrl = new Uri("wss://www.bitmex.com/realtimemd");

        /// <summary>
        /// Main Bitmex url to websocket API
        /// </summary>
        public static readonly Uri ApiWebsocketUrl = new Uri("wss://www.bitmex.com/realtime");

        /// <summary>
        /// Testnet Bitmext url to websocket API
        /// </summary>
        public static readonly Uri ApiWebsocketTestnetUrl = new Uri("wss://testnet.bitmex.com/realtime");
    }
}
