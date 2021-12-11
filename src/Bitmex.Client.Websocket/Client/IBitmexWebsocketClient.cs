using System;
using Bitmex.Client.Websocket.Requests;

namespace Bitmex.Client.Websocket.Client;

/// <summary>
/// Bitmex websocket client.
/// </summary>
public interface IBitmexWebsocketClient : IDisposable
{
    /// <summary>
    /// Serializes request and sends message via websocket client.
    /// It logs and re-throws every exception.
    /// </summary>
    /// <param name="request">Request/message to be sent</param>
    void Send<T>(T request) where T : RequestBase;

    /// <summary>
    /// Provided message streams.
    /// </summary>
    BitmexClientStreams Streams { get; }
}