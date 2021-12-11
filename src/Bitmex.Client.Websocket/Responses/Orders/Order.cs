using System;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Bitmex.Client.Websocket.Responses.Orders;

[DebuggerDisplay("Order: {Symbol}, {OrderQty}. {Price}")]
public class Order
{
    [DataMember(Name = "orderID")]
    public string OrderId { get; set; }

    [DataMember(Name = "clOrdID")]
    public string ClOrdId { get; set; }

    [DataMember(Name = "clOrdLinkID")]
    public string ClOrdLinkId {get; set; }

    public long? Account { get; set; }
    public string Symbol { get; set; }
    public BitmexSide Side { get; set; }

    public double? SimpleOrderQty { get; set; }
    public long? OrderQty {get; set; }

    public double? Price { get; set; }

    public long? DisplayQty { get; set; }
    public double? StopPx { get; set; }

    public double? PegOffsetValue { get; set; }
    public string PegPriceType { get; set; }

    public string Currency { get; set; }
    public string SettlCurrency { get; set; }

    public string OrdType { get; set; }
    public string TimeInForce { get; set; }
    public string ExecInst { get; set; }
    public string ContingencyType { get; set; }
    public string ExDestination { get; set; }
    public OrderStatus OrdStatus { get; set; }
    public string Triggered { get; set; }

    public bool? WorkingIndicator { get; set; }
    public string OrdRejReason { get; set; }
    public double? SimpleLeavesQty { get; set; }
    public long? LeavesQty { get; set; }
    public double? SimpleCumQty { get; set; }
    public long? CumQty { get; set; }
    public double? AvgPx { get; set; }
    public string MultiLegReportingType { get; set; }
    public string Text {get; set; }

    public DateTime? TransactTime { get; set; }
    public DateTime? Timestamp { get; set; }

}