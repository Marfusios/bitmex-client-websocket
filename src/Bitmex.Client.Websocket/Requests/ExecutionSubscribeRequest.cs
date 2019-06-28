namespace Bitmex.Client.Websocket.Requests
{
    public class ExecutionSubscribeRequest : SubscribeRequestBase
    {
        public override string Topic => "execution";
    }
}
