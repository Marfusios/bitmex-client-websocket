using System;

namespace Bitmex.Client.Websocket.Requests;

public class SettlementSubscribeRequest : SubscribeRequestBase
{
    /// <summary>
    /// Subscribe to all settlements
    /// </summary>
    public SettlementSubscribeRequest()
    {
        Symbol = string.Empty;
    }

    /// <summary>
    /// Subscribe to settlements for selected pair ('XBTUSD', etc)
    /// </summary>
    public SettlementSubscribeRequest(string pair)
    {
        Symbol = pair ?? throw new ArgumentNullException(nameof(pair));
    }

    public override string Topic => "settlement";
    public override string Symbol { get; }
}