using System;
using System.Runtime.Serialization;

namespace Bitmex.Client.Websocket.Requests;

public class QuoteSubscribeRequest : SubscribeRequestBase
{
    /// <summary>
    /// Subscribe to quote (top level of the book) from all pairs
    /// </summary>
    public QuoteSubscribeRequest()
    {
        Symbol = string.Empty;
    }

    /// <summary>
    /// Subscribe to quote (top level of the book) from selected pair ('XBTUSD', etc)
    /// </summary>
    public QuoteSubscribeRequest(string pair)
    {
        Symbol = pair ?? throw new ArgumentNullException(nameof(pair));
    }

    [IgnoreDataMember]
    public override string Topic => "quote";

    [IgnoreDataMember]
    public override string Symbol { get; }     
}