using System;
using System.Diagnostics;
using Bitmex.Client.Websocket.Utils;

namespace Bitmex.Client.Websocket.Responses.Liquidation
{
    [DebuggerDisplay("Liquidation")]
    public class Liquidation
    {
        public string OrderID { get; set; }
        public string Symbol { get; set; }
        public BitmexSide? Side { get; set; }
        public double? Price { get; set; }
        public long? leavesQty { get; set; }
    }
}
