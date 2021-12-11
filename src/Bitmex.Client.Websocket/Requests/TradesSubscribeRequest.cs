using System;
using System.Runtime.Serialization;

namespace Bitmex.Client.Websocket.Requests;

/// <summary>
/// Subscribe to trades stream
/// </summary>
public class TradesSubscribeRequest : SubscribeRequestBase
{
    /// <summary>
    /// Subscribe to all trades
    /// </summary>
    public TradesSubscribeRequest()
    {
        Symbol = string.Empty;
    }

    /// <summary>
    /// Subscribe to trades for selected pair ('XBTUSD', etc)
    /// </summary>
    public TradesSubscribeRequest(string pair)
    {
        Symbol = pair ?? throw new ArgumentNullException(nameof(pair));
    }

    /// <summary>
    /// Trade topic
    /// </summary>
    [IgnoreDataMember]
    public override string Topic => "trade";

    /// <inheritdoc />
    [IgnoreDataMember]
    public override string Symbol { get; }
}