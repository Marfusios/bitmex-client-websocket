using System.Runtime.Serialization;

namespace Bitmex.Client.Websocket.Requests;

public class MarginSubscribeRequest : SubscribeRequestBase
{
    [IgnoreDataMember]
    public override string Topic => "margin";
}