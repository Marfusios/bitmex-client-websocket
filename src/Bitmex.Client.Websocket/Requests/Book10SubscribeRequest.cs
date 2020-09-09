using System.Runtime.Serialization;
using Bitmex.Client.Websocket.Validations;

namespace Bitmex.Client.Websocket.Requests
{
    /// <summary>
    /// Subscribe to order book snapshot stream (only top 10 levels)
    /// </summary>
    public class Book10SubscribeRequest : SubscribeRequestBase
    {
        /// <summary>
        /// Subscribe to order book from all pairs
        /// </summary>
        public Book10SubscribeRequest()
        {
            Symbol = string.Empty;
        }

        /// <summary>
        /// Subscribe to order book from selected pair ('XBTUSD', etc)
        /// </summary>
        public Book10SubscribeRequest(string pair)
        {
            BmxValidations.ValidateInput(pair, nameof(pair));

            Symbol = pair;
        }

        /// <summary>
        /// Order book L2 topic
        /// </summary>
        [IgnoreDataMember]
        public override string Topic => "orderBook10";


        /// <inheritdoc />
        [IgnoreDataMember]
        public override string Symbol { get; }       
    }
}
