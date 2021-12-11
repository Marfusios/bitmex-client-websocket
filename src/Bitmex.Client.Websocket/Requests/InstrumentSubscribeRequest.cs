using System;
using System.Runtime.Serialization;

namespace Bitmex.Client.Websocket.Requests;

public class InstrumentSubscribeRequest: SubscribeRequestBase
{
    /// <summary>
    /// Subscribe to instrument updates including turnover and bid/ask from all pairs
    /// </summary>
    public InstrumentSubscribeRequest()
    {
        Symbol = string.Empty;
    }

    /// <summary>
    /// Subscribe to instrument updates including turnover and bid/ask from selected pair ('XBTUSD', etc)
    /// </summary>
    public InstrumentSubscribeRequest(string pair)
    {
        Symbol = pair ?? throw new ArgumentNullException(nameof(pair));
    }

    [IgnoreDataMember]
    public override string Topic => "instrument";

    [IgnoreDataMember]
    public override string Symbol { get; }     
}