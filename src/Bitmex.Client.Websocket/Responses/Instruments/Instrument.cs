using System;
using System.Diagnostics;

namespace Bitmex.Client.Websocket.Responses.Instruments;

/// <summary>
/// Trade-able Contracts, Indices, and History
/// </summary>
[DebuggerDisplay("Instrument")]
public class Instrument
{
    public string Symbol { get; set; }

    public string RootSymbol { get; set; }

    public InstrumentState State { get; set; }

    public string Type { get; set; }

    public DateTimeOffset? Listing { get; set; }

    public DateTimeOffset? Front { get; set; }

    public DateTimeOffset? Expiry { get; set; }

    public DateTimeOffset? Settle { get; set; }

    public DateTimeOffset? RelistInterval { get; set; }

    public string InverseLeg { get; set; }

    public string SellLeg { get; set; }

    public string BuyLeg { get; set; }

    public double? OptionStrikePercentage { get; set; }

    public double? OptionStrikeRound { get; set; }

    public double? OptionStrikePrice { get; set; }

    public double? OptionMultiplier { get; set; }

    public string PositionCurrency { get; set; }

    public string Underlying { get; set; }

    public string QuoteCurrency { get; set; }

    public string UnderlyingSymbol { get; set; }

    public string Reference { get; set; }

    public string ReferenceSymbol { get; set; }

    public DateTimeOffset? CalculationInterval { get; set; }

    public DateTimeOffset? PublishInterval { get; set; }

    public DateTimeOffset? PublishTime { get; set; }

    public long? MaxOrderQty { get; set; }

    public long? MaxPrice { get; set; }

    public long? LotSize { get; set; }

    public double TickSize { get; set; }

    public long? Multiplier { get; set; }

    public string SettlementCurrency { get; set; }

    public long? UnderlyingToPositionMultiplier { get; set; }

    public long? UnderlyingToSettleMultiplier { get; set; }

    public long? QuoteToSettleMultiplier { get; set; }

    public bool IsQuanto { get; set; }

    public bool IsInverse { get; set; }

    public double? InitialMargin { get; set; }

    public double? MaintenanceMargin { get; set; }

    public long? RiskLimit { get; set; }

    public long? RiskStep { get; set; }

    public double? Limit { get; set; }

    public bool Capped { get; set; }

    public bool Taxed { get; set; }

    public bool Deleverage { get; set; }

    public double? MakerFee { get; set; }

    public double? TakerFee { get; set; }

    public long? SettlementFee { get; set; }

    public long? InsuranceFee { get; set; }

    public string FundingBaseSymbol { get; set; }

    public string FundingQuoteSymbol { get; set; }

    public string FundingPremiumSymbol { get; set; }

    public DateTimeOffset? FundingTimestamp { get; set; }

    public DateTimeOffset? FundingInterval { get; set; }

    public double? FundingRate { get; set; }

    public double? IndicativeFundingRate { get; set; }

    public DateTimeOffset? RebalanceTimestamp { get; set; }

    public DateTimeOffset? RebalanceInterval { get; set; }

    public DateTimeOffset? OpeningTimestamp { get; set; }

    public DateTimeOffset? ClosingTimestamp { get; set; }

    public DateTimeOffset? SessionInterval { get; set; }

    public double? PrevClosePrice { get; set; }

    public double? LimitDownPrice { get; set; }

    public double? LimitUpPrice { get; set; }

    public double? BankruptLimitDownPrice { get; set; }

    public double? BankruptLimitUpPrice { get; set; }

    public long? PrevTotalVolume { get; set; }

    public long? TotalVolume { get; set; }

    public long? Volume { get; set; }

    public long? Volume24H { get; set; }

    public long? PrevTotalTurnover { get; set; }

    public long? TotalTurnover { get; set; }

    public long? Turnover { get; set; }

    public long? Turnover24H { get; set; }

    public long? HomeNotional24H { get; set; }

    public double? ForeignNotional24H { get; set; }

    public double PrevPrice24H { get; set; }

    public double? Vwap { get; set; }

    public double? HighPrice { get; set; }

    public double? LowPrice { get; set; }

    public double LastPrice { get; set; }

    public double? LastPriceProtected { get; set; }

    public BitmexTickDirection LastTickDirection { get; set; }

    public double LastChangePercentage { get; set; }

    public double? BidPrice { get; set; }

    public double? MidPrice { get; set; }

    public double? AskPrice { get; set; }

    public double? ImpactBidPrice { get; set; }

    public double? ImpactMidPrice { get; set; }

    public double? ImpactAskPrice { get; set; }

    public bool HasLiquidity { get; set; }

    public long? OpenInterest { get; set; }

    public long OpenValue { get; set; }

    public InstrumentFairMethod FairMethod { get; set; }

    public double? FairBasisRate { get; set; }

    public double? FairBasis { get; set; }

    public double? FairPrice { get; set; }

    public InstrumentMarkMethod MarkMethod { get; set; }

    public double MarkPrice { get; set; }

    public long? IndicativeTaxRate { get; set; }

    public double? IndicativeSettlePrice { get; set; }

    public double? OptionUnderlyingPrice { get; set; }

    public double? SettledPrice { get; set; }

    public DateTimeOffset Timestamp { get; set; }
}