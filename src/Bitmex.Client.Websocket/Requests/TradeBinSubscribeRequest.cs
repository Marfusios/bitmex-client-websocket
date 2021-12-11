using System;
using System.Runtime.Serialization;

namespace Bitmex.Client.Websocket.Requests;

public class TradeBinSubscribeRequest : SubscribeRequestBase
{
    /// <summary>
    /// Subscribe to all trades
    /// </summary>
    public TradeBinSubscribeRequest()
    {
        Symbol = string.Empty;
        Topic = "tradeBin1m";
    }

    /// <summary>
    /// Subscribe to trades for selected pair ('XBTUSD', etc)
    /// </summary>
    public TradeBinSubscribeRequest(string sizeArg, string pair)
    {
        Symbol = pair ?? throw new ArgumentNullException(nameof(pair));
        Size = sizeArg;
        Topic = "tradeBin" + Size;
    }

    [IgnoreDataMember]
    public string Size { get; } = "1m";

    [IgnoreDataMember]
    public override string Topic { get; }

    [IgnoreDataMember]
    public override string Symbol { get; }

}