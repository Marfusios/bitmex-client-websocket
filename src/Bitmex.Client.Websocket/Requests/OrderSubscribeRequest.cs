using System.Runtime.Serialization;

namespace Bitmex.Client.Websocket.Requests;

public class OrderSubscribeRequest : SubscribeRequestBase
{
    [IgnoreDataMember]
    public override string Topic => "order";
}