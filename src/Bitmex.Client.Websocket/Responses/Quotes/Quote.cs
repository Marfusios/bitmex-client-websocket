using System;

namespace Bitmex.Client.Websocket.Responses.Quotes
{
    /// <summary>
    /// Top level of the Bitmex order book
    /// </summary>
    public class Quote
    {
        public DateTime? Timestamp { get; set; }
        public string Symbol { get; set; }

        public long? BidSize { get; set; }
        public double? BidPrice { get; set; }

        public long? AskSize { get; set; }
        public double? AskPrice { get; set; }
    }
}
