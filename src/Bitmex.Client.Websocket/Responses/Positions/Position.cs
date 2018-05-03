using System;
using System.Diagnostics;

namespace Bitmex.Client.Websocket.Responses.Positions
{
    [DebuggerDisplay("Postion: {Symbol}, {Currency}. {LastPrice}, {CurrentQty}")]
    public class Position
    {
        public long Account { get; set; }
        public string Symbol {get; set; }
        public string Currency { get; set; }

        public string Underlying { get; set; }
        public string QuoteCurrency { get; set; }

        public double? Commission { get; set; }

        public double? InitMarginReq { get; set; }
        public double? MaintMarginReq { get; set; }

        public long? RiskLimit { get; set; }
        public double? Leverage { get; set; }
        public bool? CrossMargin { get; set; }
        public double? DeleveragePercentile { get; set; }

        public long? RebalancedPnl { get; set; }
        public long? PrevRealisedPnl { get; set; }
        public long? PrevUnrealisedPnl { get; set; }
        public double? PrevClosePrice { get; set; }

        public DateTime? OpeningTimestamp { get; set; }
        public long? OpeningQty { get; set; }
        public long? OpeningCost { get; set; }
        public long? OpeningComm { get; set; }
        public long? OpenOrderBuyQty { get; set; }
        public long? OpenOrderBuyCost { get; set; }
        public long? OpenOrderBuyPremium { get; set; }
        public long? OpenOrderSellQty { get; set; }
        public long? OpenOrderSellCost { get; set; }
        public long? OpenOrderSellPremium { get; set; }

        public long? ExecBuyQty { get; set; }
        public long? ExecBuyCost { get; set; }
        public long? ExecSellQty { get; set; }
        public long? ExecSellCost { get; set; }
        public long? ExecQty { get; set; }
        public long? ExecCost { get; set; }
        public long? ExecComm { get; set; }

        public DateTime? CurrentTimestamp { get; set; }
        public long? CurrentQty { get; set; }
        public long? CurrentCost { get; set; }
        public long? CurrentComm { get; set; }

        public long? RealisedCost { get; set; }
        public long? UnrealisedCost { get; set; }

        public long? GrossOpenCost { get; set; }
        public long? GrossOpenPremium { get; set; }
        public long? GrossExecCost { get; set; }

        public bool? IsOpen { get; set; }
        public double? MarkPrice { get; set; }
        public long? MarkValue { get; set; }
        public long? RiskValue { get; set; }
        public double? HomeNotional { get; set; }
        public double? ForeignNotional { get; set; }

        public string PosState { get; set; }
        public long? PosCost { get; set; }
        public long? PosCost2 { get; set; }
        public long? PosCross { get; set; }
        public long? PosInit { get; set; }
        public long? PosComm { get; set; }
        public long? PosLoss { get; set; }
        public long? PosMargin { get; set; }
        public long? PosMaint { get; set; }
        public long? PosAllowance { get; set; }

        public long? TaxableMargin { get; set; }
        public long? InitMargin { get; set; }
        public long? MaintMargin { get; set; }
        public long? SessionMargin { get; set; }
        public long? TargetExcessMargin { get; set; }
        public long? VarMargin { get; set; }
        public long? RealisedGrossPnl { get; set; }
        public long? RealisedTax { get; set; }
        public long? RealisedPnl { get; set; }

        public long? UnrealisedGrossPnl { get; set; }
        public long? LongBankrupt { get; set; }
        public long? ShortBankrupt { get; set; }

        public long? TaxBase { get; set; }
        public double? IndicativeTaxRate { get; set; }
        public long? IndicativeTax { get; set; }
        public long? UnrealisedTax { get; set; }
        public long? UnrealisedPnl { get; set; }

        public double? UnrealisedPnlPcnt { get; set; }
        public double? UnrealisedRoePcnt { get; set; }
        public double? SimpleQty { get; set; }
        public double? SimpleCost { get; set; }
        public double? SimpleValue { get; set; }
        public double? SimplePnl { get; set; }
        public double? SimplePnlPcnt { get; set; }
        public double? AvgCostPrice { get; set; }
        public double? AvgEntryPrice { get; set; }
        public double? BreakEvenPrice { get; set; }
        public double? MarginCallPrice { get; set; }
        public double? LiquidationPrice { get; set; }
        public double? BankruptPrice { get; set; }
        public DateTime? Timestamp { get; set; }
        public double? LastPrice { get; set; }
        public long? LastValue { get; set; }
    }
}
