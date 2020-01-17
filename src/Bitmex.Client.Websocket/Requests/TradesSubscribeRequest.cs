using System.Runtime.Serialization;
using Bitmex.Client.Websocket.Validations;

namespace Bitmex.Client.Websocket.Requests
{
    /// <summary>
    /// Subscribe to trades stream
    /// </summary>
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

        /// <summary>
        /// Trade topic
        /// </summary>
        [IgnoreDataMember]
        public override string Topic => "trade";

        /// <inheritdoc />
        [IgnoreDataMember]
        public override string Symbol { get; }
    }
}
