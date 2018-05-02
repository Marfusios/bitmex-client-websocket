namespace Bitmex.Client.Websocket.Requests
{
    public class WalletSubscribeRequest : SubscribeRequestBase
    {
        public override string Topic => "wallet";
    }
}
