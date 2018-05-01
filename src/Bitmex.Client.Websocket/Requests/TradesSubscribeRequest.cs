using Bitmex.Client.Websocket.Validations;
using Newtonsoft.Json;

namespace Bitmex.Client.Websocket.Requests
{
    public class TradesSubscribeRequest : SubscribeRequestBase
    {
        /// <summary>
        /// Subscribe to all trades
        /// </summary>
        public TradesSubscribeRequest()
        {
            Symbol = string.Empty;
        }

        /// <summary>
        /// Subscribe to trades for selected pair ('XBTUSD', etc)
        /// </summary>
        public TradesSubscribeRequest(string pair)
        {
            BmxValidations.ValidateInput(pair, nameof(pair));

            Symbol = pair;
        }

        public override string Topic => string.IsNullOrWhiteSpace(Symbol) ? "trade" : $"trade:{Symbol}";

        [JsonIgnore]
        public string Symbol { get; }
    }
}
