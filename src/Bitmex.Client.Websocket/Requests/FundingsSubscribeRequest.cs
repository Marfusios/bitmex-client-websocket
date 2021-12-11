using System;
using System.Runtime.Serialization;

namespace Bitmex.Client.Websocket.Requests;

/// <summary>
/// Subscribe to trades stream
/// </summary>
public class FundingsSubscribeRequest : SubscribeRequestBase
{
    /// <summary>
    /// Subscribe to all fundings
    /// </summary>
    public FundingsSubscribeRequest()
    {
        Symbol = string.Empty;
    }

    /// <summary>
    /// Subscribe to fundings for selected pair, e.g. 'XBTUSD'
    /// </summary>
    public FundingsSubscribeRequest(string pair)
    {
        Symbol = pair ?? throw new ArgumentNullException(nameof(pair));
    }

    /// <summary>
    /// Funding topic
    /// </summary>
    [IgnoreDataMember]
    public override string Topic => "funding";

    [IgnoreDataMember]
    public override string Symbol { get; }
}