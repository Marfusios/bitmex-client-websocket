using Bitmex.Client.Websocket.Validations;
using Newtonsoft.Json;

namespace Bitmex.Client.Websocket.Requests
{
    public class SettlementSubscribeRequest : SubscribeRequestBase
    {
        public SettlementSubscribeRequest(string pair)
        {
            BmxValidations.ValidateInput(pair, nameof(pair));

            Symbol = pair;
        }

        public override string Topic => $"settlement:{Symbol}";

        [JsonIgnore]
        public string Symbol { get; }
    }
}
