using System;
using System.Diagnostics;

namespace Bitmex.Client.Websocket.Responses.Margins
{
    /// <summary>
    /// Information about your margin
    /// </summary>
    [DebuggerDisplay("Margin: {Account}, {Currency}")]
    public class Margin
    {
        /// <summary>
        /// Account identification
        /// </summary>
        public long? Account { get; set; }

        /// <summary>
        /// Current `Amount` currency, for example `XBt` which is satoshi
        /// </summary>
        public string Currency { get; set; }

        public long? RiskLimit { get; set; }
        public string PrevState { get; set; }
        public string State { get; set; }
        public string Action { get; set; }

        /// <summary>
        /// Current amount in satoshis.
        /// Use `BitmexConverter` to convert value into BTC. 
        /// </summary>
        public long? Amount { get; set; }

        public long? PendingCredit { get; set; }
        public long? PendingDebit { get; set; }
        public long? ConfirmedDebit { get; set; }
        public long? PrevRealisedPnl { get; set; }
        public long? PrevUnrealisedPnl { get; set; }
        public long? GrossComm { get; set; }
        public long? GrossOpenCost { get; set; }
        public long? GrossOpenPremium { get; set; }
        public long? GrossExecCost { get; set; }
        public long? GrossMarkValue { get; set; }
        public long? RiskValue { get; set; }
        public long? TaxableMargin { get; set; }
        public long? InitMargin { get; set; }
        public long? MaintMargin { get; set; }
        public long? SessionMargin { get; set; }
        public long? TargetExcessMargin { get; set; }
        public long? VarMargin { get; set; }
        public long? RealisedPnl { get; set; }
        public long? UnrealisedPnl { get; set; }
        public long? IndicativeTax { get; set; }
        public long? UnrealisedProfit { get; set; }
        public long? SyntheticMargin { get; set; }

        /// <summary>
        /// Current wallet balance in satoshis.
        /// Use `BitmexConverter` to convert value into BTC. 
        /// </summary>
        public long? WalletBalance { get; set; }

        /// <summary>
        /// Current margin balance in satoshis.
        /// Use `BitmexConverter` to convert value into BTC. 
        /// </summary>
        public long? MarginBalance { get; set; }

        /// <summary>
        /// Current available margin balance in satoshis.
        /// Use `BitmexConverter` to convert value into BTC. 
        /// </summary>
        public long? AvailableMargin { get; set; }

        public float? MarginBalancePcnt { get; set; }
        public float? MarginLeverage { get; set; }
        public float? MarginUsedPcnt { get; set; }
        public long? ExcessMargin { get; set; }
        public float? ExcessMarginPcnt { get; set; }
        public long? WithdrawableMargin { get; set; }
        public DateTime? Timestamp { get; set; }
        public long? GrossLastValue { get; set; }
        public float? Commission { get; set; }
    }
}
