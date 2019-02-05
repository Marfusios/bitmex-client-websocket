using Bitmex.Client.Websocket.Validations;

namespace Bitmex.Client.Websocket.Requests
{
    /// <summary>
    /// Subscribe to order book stream
    /// </summary>
    public class BookSubscribeRequest : SubscribeRequestBase
    {
        /// <summary>
        /// Subscribe to order book from all pairs
        /// </summary>
        public BookSubscribeRequest()
        {
            Symbol = string.Empty;
        }

        /// <summary>
        /// Subscribe to order book from selected pair ('XBTUSD', etc)
        /// </summary>
        public BookSubscribeRequest(string pair)
        {
            BmxValidations.ValidateInput(pair, nameof(pair));

            Symbol = pair;
        }

        /// <summary>
        /// Order book L2 topic
        /// </summary>
        public override string Topic => "orderBookL2";


        /// <inheritdoc />
        public override string Symbol { get; }       
    }
}
