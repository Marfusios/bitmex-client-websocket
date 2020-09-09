using System.Runtime.Serialization;
using Bitmex.Client.Websocket.Validations;

namespace Bitmex.Client.Websocket.Requests
{
    /// <summary>
    /// Subscribe to order book L2 stream (only top 25 levels)
    /// </summary>
    public class Book25SubscribeRequest : SubscribeRequestBase
    {
        /// <summary>
        /// Subscribe to order book from all pairs
        /// </summary>
        public Book25SubscribeRequest()
        {
            Symbol = string.Empty;
        }

        /// <summary>
        /// Subscribe to order book from selected pair ('XBTUSD', etc)
        /// </summary>
        public Book25SubscribeRequest(string pair)
        {
            BmxValidations.ValidateInput(pair, nameof(pair));

            Symbol = pair;
        }

        /// <summary>
        /// Order book L2 topic
        /// </summary>
        [IgnoreDataMember]
        public override string Topic => "orderBookL2_25";


        /// <inheritdoc />
        [IgnoreDataMember]
        public override string Symbol { get; }       
    }
}
