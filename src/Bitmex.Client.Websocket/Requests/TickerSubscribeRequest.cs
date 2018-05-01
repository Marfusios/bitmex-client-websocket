using Bitmex.Client.Websocket.Validations;

namespace Bitmex.Client.Websocket.Requests
{
    public class TickerSubscribeRequest : SubscribeRequestBase
    {
        public TickerSubscribeRequest(string pair)
        {
            BmxValidations.ValidateInput(pair, nameof(pair));

            Symbol = pair;
        }

        public override string Topic => "ticker";
        public string Symbol { get; }
    }
}
