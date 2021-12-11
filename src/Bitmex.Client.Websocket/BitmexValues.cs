using System;

namespace Bitmex.Client.Websocket;

/// <summary>
/// Bitmex static urls
/// </summary>
public static class BitmexValues
{
    /// <summary>
    /// Main Bitmex url to websocket API
    /// </summary>
    public static readonly Uri ApiWebsocketUrl = new("wss://www.bitmex.com/realtime");

    /// <summary>
    /// Testnet Bitmext url to websocket API
    /// </summary>
    public static readonly Uri ApiWebsocketTestnetUrl = new("wss://testnet.bitmex.com/realtime");
}