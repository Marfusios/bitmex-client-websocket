using System;
using System.Runtime.Serialization;

namespace Bitmex.Client.Websocket.Requests;

/// <summary>
/// Subscribe to order book L2 stream
/// </summary>
public class BookSubscribeRequest : SubscribeRequestBase
{
    /// <summary>
    /// Subscribe to order book from all pairs
    /// </summary>
    public BookSubscribeRequest()
    {
        Symbol = string.Empty;
    }

    /// <summary>
    /// Subscribe to order book from selected pair ('XBTUSD', etc)
    /// </summary>
    public BookSubscribeRequest(string pair)
    {
        Symbol = pair ?? throw new ArgumentNullException(nameof(pair));
    }

    /// <summary>
    /// Order book L2 topic
    /// </summary>
    [IgnoreDataMember]
    public override string Topic => "orderBookL2";


    /// <inheritdoc />
    [IgnoreDataMember]
    public override string Symbol { get; }       
}