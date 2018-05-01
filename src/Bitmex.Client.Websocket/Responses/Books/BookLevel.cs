namespace Bitmex.Client.Websocket.Responses.Books
{
    /// <summary>
    /// The state of the Bitmex order book
    /// </summary>
    public class BookLevel
    {
        /// <summary>
        /// Order book level id (combination of price and symbol)
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Target symbol
        /// </summary>
        public string Symbol { get; set; }

        public BitmexSide Side { get; set; }

        /// <summary>
        /// Available only for 'partial', 'insert' and 'update' action
        /// </summary>
        public long? Size { get; set; }

        /// <summary>
        /// Available only for 'partial' and 'insert' action, use Id otherwise
        /// </summary>
        public double? Price { get; set; }
    }
}
