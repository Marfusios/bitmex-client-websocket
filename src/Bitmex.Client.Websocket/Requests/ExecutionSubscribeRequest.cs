using System.Runtime.Serialization;

namespace Bitmex.Client.Websocket.Requests
{
    public class ExecutionSubscribeRequest : SubscribeRequestBase
    {
        [IgnoreDataMember]
        public override string Topic => "execution";
    }
}
