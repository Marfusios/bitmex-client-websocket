using System.Runtime.Serialization;

namespace Bitmex.Client.Websocket.Requests
{
    public class WalletSubscribeRequest : SubscribeRequestBase
    {
        [IgnoreDataMember]
        public override string Topic => "wallet";
    }
}
