using System;
using System.Diagnostics;
using Newtonsoft.Json;

namespace Bitmex.Client.Websocket.Responses.Instruments
{
    /// <summary>
    /// Trade-able Contracts, Indices, and History
    /// </summary>
    [DebuggerDisplay("Instrument")]
    public class Instrument
    {
        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("rootSymbol")]
        public string RootSymbol { get; set; }

        [JsonProperty("state")]
        public InstrumentState State { get; set; }

        [JsonProperty("typ")]
        public string Type { get; set; }

        [JsonProperty("listing")]
        public DateTimeOffset? Listing { get; set; }

        [JsonProperty("front")]
        public DateTimeOffset? Front { get; set; }

        [JsonProperty("expiry")]
        public DateTimeOffset? Expiry { get; set; }

        [JsonProperty("settle")]
        public DateTimeOffset? Settle { get; set; }

        [JsonProperty("relistInterval")]
        public DateTimeOffset? RelistInterval { get; set; }

        [JsonProperty("inverseLeg")]
        public string InverseLeg { get; set; }

        [JsonProperty("sellLeg")]
        public string SellLeg { get; set; }

        [JsonProperty("buyLeg")]
        public string BuyLeg { get; set; }

        [JsonProperty("optionStrikePcnt")]
        public double? OptionStrikePercentage { get; set; }

        [JsonProperty("optionStrikeRound")]
        public double? OptionStrikeRound { get; set; }

        [JsonProperty("optionStrikePrice")]
        public double? OptionStrikePrice { get; set; }

        [JsonProperty("optionMultiplier")]
        public double? OptionMultiplier { get; set; }

        [JsonProperty("positionCurrency")]
        public string PositionCurrency { get; set; }

        [JsonProperty("underlying")]
        public string Underlying { get; set; }

        [JsonProperty("quoteCurrency")]
        public string QuoteCurrency { get; set; }

        [JsonProperty("underlyingSymbol")]
        public string UnderlyingSymbol { get; set; }

        [JsonProperty("reference")]
        public string Reference { get; set; }

        [JsonProperty("referenceSymbol")]
        public string ReferenceSymbol { get; set; }

        [JsonProperty("calcInterval")]
        public DateTimeOffset? CalculationInterval { get; set; }

        [JsonProperty("publishInterval")]
        public DateTimeOffset? PublishInterval { get; set; }

        [JsonProperty("publishTime")]
        public DateTimeOffset? PublishTime { get; set; }

        [JsonProperty("maxOrderQty")]
        public long? MaxOrderQty { get; set; }

        [JsonProperty("maxPrice")]
        public long? MaxPrice { get; set; }

        [JsonProperty("lotSize")]
        public long? LotSize { get; set; }

        [JsonProperty("tickSize")]
        public double TickSize { get; set; }

        [JsonProperty("multiplier")]
        public long? Multiplier { get; set; }

        [JsonProperty("settlCurrency")]
        public string SettlementCurrency { get; set; }

        [JsonProperty("underlyingToPositionMultiplier")]
        public long? UnderlyingToPositionMultiplier { get; set; }

        [JsonProperty("underlyingToSettleMultiplier")]
        public long? UnderlyingToSettleMultiplier { get; set; }

        [JsonProperty("quoteToSettleMultiplier")]
        public long? QuoteToSettleMultiplier { get; set; }

        [JsonProperty("isQuanto")]
        public bool IsQuanto { get; set; }

        [JsonProperty("isInverse")]
        public bool IsInverse { get; set; }

        [JsonProperty("initMargin")]
        public double? InitialMargin { get; set; }

        [JsonProperty("maintMargin")]
        public double? MaintenanceMargin { get; set; }

        [JsonProperty("riskLimit")]
        public long? RiskLimit { get; set; }

        [JsonProperty("riskStep")]
        public long? RiskStep { get; set; }

        [JsonProperty("limit")]
        public double? Limit { get; set; }

        [JsonProperty("capped")]
        public bool Capped { get; set; }

        [JsonProperty("taxed")]
        public bool Taxed { get; set; }

        [JsonProperty("deleverage")]
        public bool Deleverage { get; set; }

        [JsonProperty("makerFee")]
        public double? MakerFee { get; set; }

        [JsonProperty("takerFee")]
        public double? TakerFee { get; set; }

        [JsonProperty("settlementFee")]
        public long? SettlementFee { get; set; }

        [JsonProperty("insuranceFee")]
        public long? InsuranceFee { get; set; }

        [JsonProperty("fundingBaseSymbol")]
        public string FundingBaseSymbol { get; set; }

        [JsonProperty("fundingQuoteSymbol")]
        public string FundingQuoteSymbol { get; set; }

        [JsonProperty("fundingPremiumSymbol")]
        public string FundingPremiumSymbol { get; set; }

        [JsonProperty("fundingTimestamp")]
        public DateTimeOffset? FundingTimestamp { get; set; }

        [JsonProperty("fundingInterval")]
        public DateTimeOffset? FundingInterval { get; set; }

        [JsonProperty("fundingRate")]
        public double? FundingRate { get; set; }

        [JsonProperty("indicativeFundingRate")]
        public double? IndicativeFundingRate { get; set; }

        [JsonProperty("rebalanceTimestamp")]
        public DateTimeOffset? RebalanceTimestamp { get; set; }

        [JsonProperty("rebalanceInterval")]
        public DateTimeOffset? RebalanceInterval { get; set; }

        [JsonProperty("openingTimestamp")]
        public DateTimeOffset? OpeningTimestamp { get; set; }

        [JsonProperty("closingTimestamp")]
        public DateTimeOffset? ClosingTimestamp { get; set; }

        [JsonProperty("sessionInterval")]
        public DateTimeOffset? SessionInterval { get; set; }

        [JsonProperty("prevClosePrice")]
        public double? PrevClosePrice { get; set; }

        [JsonProperty("limitDownPrice")]
        public double? LimitDownPrice { get; set; }

        [JsonProperty("limitUpPrice")]
        public double? LimitUpPrice { get; set; }

        [JsonProperty("bankruptLimitDownPrice")]
        public double? BankruptLimitDownPrice { get; set; }

        [JsonProperty("bankruptLimitUpPrice")]
        public double? BankruptLimitUpPrice { get; set; }

        [JsonProperty("prevTotalVolume")]
        public long? PrevTotalVolume { get; set; }

        [JsonProperty("totalVolume")]
        public long? TotalVolume { get; set; }

        [JsonProperty("volume")]
        public long? Volume { get; set; }

        [JsonProperty("volume24h")]
        public long? Volume24H { get; set; }

        [JsonProperty("prevTotalTurnover")]
        public long? PrevTotalTurnover { get; set; }

        [JsonProperty("totalTurnover")]
        public long? TotalTurnover { get; set; }

        [JsonProperty("turnover")]
        public long? Turnover { get; set; }

        [JsonProperty("turnover24h")]
        public long? Turnover24H { get; set; }

        [JsonProperty("homeNotional24h")]
        public long? HomeNotional24H { get; set; }

        [JsonProperty("foreignNotional24h")]
        public double? ForeignNotional24H { get; set; }

        [JsonProperty("prevPrice24h")]
        public double PrevPrice24H { get; set; }

        [JsonProperty("vwap")]
        public double? Vwap { get; set; }

        [JsonProperty("highPrice")]
        public double? HighPrice { get; set; }

        [JsonProperty("lowPrice")]
        public double? LowPrice { get; set; }

        [JsonProperty("lastPrice")]
        public double LastPrice { get; set; }

        [JsonProperty("lastPriceProtected")]
        public double? LastPriceProtected { get; set; }

        [JsonProperty("lastTickDirection")]
        public BitmexTickDirection LastTickDirection { get; set; }

        [JsonProperty("lastChangePcnt")]
        public double LastChangePercentage { get; set; }

        [JsonProperty("bidPrice")]
        public double? BidPrice { get; set; }

        [JsonProperty("midPrice")]
        public double? MidPrice { get; set; }

        [JsonProperty("askPrice")]
        public double? AskPrice { get; set; }

        [JsonProperty("impactBidPrice")]
        public double? ImpactBidPrice { get; set; }

        [JsonProperty("impactMidPrice")]
        public double? ImpactMidPrice { get; set; }

        [JsonProperty("impactAskPrice")]
        public double? ImpactAskPrice { get; set; }

        [JsonProperty("hasLiquidity")]
        public bool HasLiquidity { get; set; }

        [JsonProperty("openInterest")]
        public long? OpenInterest { get; set; }

        [JsonProperty("openValue")]
        public long OpenValue { get; set; }

        [JsonProperty("fairMethod")]
        public InstrumentFairMethod FairMethod { get; set; }

        [JsonProperty("fairBasisRate")]
        public double? FairBasisRate { get; set; }

        [JsonProperty("fairBasis")]
        public double? FairBasis { get; set; }

        [JsonProperty("fairPrice")]
        public double? FairPrice { get; set; }

        [JsonProperty("markMethod")]
        public InstrumentMarkMethod MarkMethod { get; set; }

        [JsonProperty("markPrice")]
        public double MarkPrice { get; set; }

        [JsonProperty("indicativeTaxRate")]
        public long? IndicativeTaxRate { get; set; }

        [JsonProperty("indicativeSettlePrice")]
        public double? IndicativeSettlePrice { get; set; }

        [JsonProperty("optionUnderlyingPrice")]
        public double? OptionUnderlyingPrice { get; set; }

        [JsonProperty("settledPrice")]
        public double? SettledPrice { get; set; }

        [JsonProperty("timestamp")]
        public DateTimeOffset Timestamp { get; set; }
    }

}
