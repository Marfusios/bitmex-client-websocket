namespace Bitmex.Client.Websocket.Requests
{
    public class PositionSubscribeRequest : SubscribeRequestBase
    {
        public override string Topic => "position";
    }
}
