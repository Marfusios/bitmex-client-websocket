namespace Bitmex.Client.Websocket.Requests
{
    public class OrderSubscribeRequest : SubscribeRequestBase
    {
        public override string Topic => "order";
    }
}
