using Bitmex.Client.Websocket.Validations;
using Newtonsoft.Json;

namespace Bitmex.Client.Websocket.Requests
{
    public class BookSubscribeRequest: SubscribeRequestBase
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

        public override string Topic => "orderBookL2";
        public override string Symbol { get; }       
    }
}
