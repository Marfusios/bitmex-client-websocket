using System;
using System.Diagnostics;

namespace Bitmex.Client.Websocket.Responses.Executions;

[DebuggerDisplay("Execution: {Symbol}, {OrderQty}. {Price}")]
public partial class Execution
{
    public Guid ExecID { get; set; }
    public Guid? OrderID { get; set; }
    public string ClOrdID { get; set; }
    public string ClOrdLinkID { get; set; }
    public double? Account { get; set; }
    public string Symbol { get; set; }
    public string Side { get; set; }
    public double? LastQty { get; set; }
    public double? LastPx { get; set; }
    public double? UnderlyingLastPx { get; set; }
    public string LastMkt { get; set; }
    public string LastLiquidityInd { get; set; }
    public double? SimpleOrderQty { get; set; }
    public double? OrderQty { get; set; }
    public double? Price { get; set; }
    public double? DisplayQty { get; set; }
    public double? StopPx { get; set; }
    public double? PegOffsetValue { get; set; }
    public string PegPriceType { get; set; }
    public string Currency { get; set; }
    public string SettlCurrency { get; set; }
    public string ExecType { get; set; }
    public string OrdType { get; set; }
    public string TimeInForce { get; set; }
    public string ExecInst { get; set; }
    public string ContingencyType { get; set; }
    public string ExDestination { get; set; }
    public string OrdStatus { get; set; }
    public string Triggered { get; set; }
    public bool? WorkingIndicator { get; set; }
    public string OrdRejReason { get; set; }
    public double? SimpleLeavesQty { get; set; }
    public double? LeavesQty { get; set; }
    public double? SimpleCumQty { get; set; }
    public double? CumQty { get; set; }
    public double? AvgPx { get; set; }
    public double? Commission { get; set; }
    public string TradePublishIndicator { get; set; }
    public string MultiLegReportingType { get; set; }
    public string Text { get; set; }
    public Guid? TrdMatchID { get; set; }
    public double? ExecCost { get; set; }
    public double? ExecComm { get; set; }
    public double? HomeNotional { get; set; }
    public double? ForeignNotional { get; set; }
    public DateTimeOffset? TransactTime { get; set; }
    public DateTimeOffset? Timestamp { get; set; }
}