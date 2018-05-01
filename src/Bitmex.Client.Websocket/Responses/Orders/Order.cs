using System.Diagnostics;
using Newtonsoft.Json;

namespace Bitmex.Client.Websocket.Responses.Orders
{
    [DebuggerDisplay("Order: {Id}/{Cid} - {Symbol} - {Amount}")]
    [JsonConverter(typeof(OrderConverter))]
    public class Order
    {
        public long Id { get; set; }
        public long? Gid { get; set; }
        public long Cid { get; set; }
        public string Symbol { get; set; }
        public long? MtsCreate { get; set; }
        public long? MtsUpdate { get; set; }
        public double? Amount { get; set; }
        public double? AmountOrig { get; set; }
        public OrderType Type { get; set; }
        public OrderType TypePrev { get; set; }
        public int? Flags { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public double? Price { get; set; }
        public double? PriceAvg { get; set; }
        public double? PriceTrailing { get; set; }
        public double? PriceAuxLimit { get; set; }
        public int? Notify { get; set; }
        public int? Hidden { get; set; }
        public int? PlacedId { get; set; }

        public string Pair => !string.IsNullOrWhiteSpace(Symbol) && Symbol.Length > 6 ? Symbol.Remove(0, 1) : string.Empty;
        public string PrimarySymbol => !string.IsNullOrWhiteSpace(Pair) && Pair.Length > 5 ? Pair.Substring(0, 3) : string.Empty;
        public string SecondarySymbol => !string.IsNullOrWhiteSpace(Pair) && Pair.Length > 5 ? Pair.Substring(3, 3) : string.Empty;
    }
}
