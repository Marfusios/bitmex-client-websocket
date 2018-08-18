using Bitmex.Client.Websocket.Validations;

namespace Bitmex.Client.Websocket.Requests
{
    public class TradeBinSubscribeRequest : SubscribeRequestBase
    {
        /// <summary>
        /// Subscribe to all trades
        /// </summary>
        public TradeBinSubscribeRequest()
        {
            Symbol = string.Empty;
            Topic = "tradeBin1m";
        }

        /// <summary>
        /// Subscribe to trades for selected pair ('XBTUSD', etc)
        /// </summary>
        public TradeBinSubscribeRequest(string sizeArg, string pair)
        {
            BmxValidations.ValidateInput(pair, nameof(pair));

            Symbol = pair;
            Size = sizeArg;
            Topic = "tradeBin" + Size;
        }

        public string Size { get; } = "1m";
        public override string Topic { get; }
        public override string Symbol { get; }

    }
}
