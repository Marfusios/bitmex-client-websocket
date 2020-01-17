using System.Runtime.Serialization;
using Bitmex.Client.Websocket.Validations;

namespace Bitmex.Client.Websocket.Requests
{
    public class QuoteSubscribeRequest : SubscribeRequestBase
    {
        /// <summary>
        /// Subscribe to quote (top level of the book) from all pairs
        /// </summary>
        public QuoteSubscribeRequest()
        {
            Symbol = string.Empty;
        }

        /// <summary>
        /// Subscribe to quote (top level of the book) from selected pair ('XBTUSD', etc)
        /// </summary>
        public QuoteSubscribeRequest(string pair)
        {
            BmxValidations.ValidateInput(pair, nameof(pair));

            Symbol = pair;
        }

        [IgnoreDataMember]
        public override string Topic => "quote";

        [IgnoreDataMember]
        public override string Symbol { get; }     
    }
}
