namespace Bitmex.Client.Websocket.Requests
{
    public class LiquidationSubscribeRequest : SubscribeRequestBase
    {
        public override string Topic => "liquidation";
    }
}
