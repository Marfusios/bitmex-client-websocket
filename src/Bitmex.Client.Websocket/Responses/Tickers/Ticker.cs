using Newtonsoft.Json;

namespace Bitmex.Client.Websocket.Responses.Tickers
{
    [JsonConverter(typeof(TickerConverter))]
    public class Ticker : ResponseBase
    {
        /// <summary>
        /// Price of last highest bid
        /// </summary>
        public double Bid { get; set; }

        /// <summary>
        /// Size of the last highest bid
        /// </summary>
        public double BidSize { get; set; }

        /// <summary>
        /// Price of last lowest ask
        /// </summary>
        public double Ask { get; set; }

        /// <summary>
        /// Size of the last lowest ask
        /// </summary>
        public double AskSize { get; set; }

        /// <summary>
        /// Amount that the last price has changed since yesterday
        /// </summary>
        public double DailyChange { get; set; }

        /// <summary>
        /// Amount that the price has changed expressed in percentage terms
        /// </summary>
        public double DailyChangePercent { get; set; }

        /// <summary>
        /// Price of the last trade
        /// </summary>
        public double LastPrice { get; set; }

        /// <summary>
        /// Daily volume
        /// </summary>
        public double Volume { get; set; }

        /// <summary>
        /// Daily high
        /// </summary>
        public double High { get; set; }

        /// <summary>
        /// Daily low
        /// </summary>
        public double Low { get; set; }

        /// <summary>
        /// Target pair
        /// </summary>
        [JsonIgnore]
        public string Pair { get; set; }
    }
}