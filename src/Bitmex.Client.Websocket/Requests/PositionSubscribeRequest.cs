using System.Runtime.Serialization;

namespace Bitmex.Client.Websocket.Requests;

public class PositionSubscribeRequest : SubscribeRequestBase
{
    [IgnoreDataMember]
    public override string Topic => "position";
}