using System.Runtime.Serialization;

namespace Bitmex.Client.Websocket.Requests
{
    public class LiquidationSubscribeRequest : SubscribeRequestBase
    {
        [IgnoreDataMember]
        public override string Topic => "liquidation";
    }
}
