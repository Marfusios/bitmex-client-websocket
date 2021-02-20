using Bitmex.Client.Websocket.Requests;
using System;

namespace Bitmex.Client.Websocket.Client
{
    public interface IBitmexWebsocketClient : IDisposable
    {
        BitmexClientStreams Streams { get; }
        void Send<T>(T request) where T : RequestBase;
        void Authenticate(string apiKey, string apiSecret);
    }
}
