using Bitmex.Client.Websocket.Validations;

namespace Bitmex.Client.Websocket.Requests
{
    /// <summary>
    /// Subscribe to trades stream
    /// </summary>
    public class FundingsSubscribeRequest : SubscribeRequestBase
    {
        /// <summary>
        /// Subscribe to all fundings
        /// </summary>
        public FundingsSubscribeRequest()
        {
            Symbol = string.Empty;
        }

        /// <summary>
        /// Subscribe to fundings for selected pair, e.g. 'XBTUSD'
        /// </summary>
        public FundingsSubscribeRequest(string pair)
        {
            BmxValidations.ValidateInput(pair, nameof(pair));
            Symbol = pair;
        }

        /// <summary>
        /// Funding topic
        /// </summary>
        public override string Topic => "funding";

        /// <inheritdoc />
        public override string Symbol { get; }
    }
}