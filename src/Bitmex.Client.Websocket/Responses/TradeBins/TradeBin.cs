using System;

namespace Bitmex.Client.Websocket.Responses.TradeBins;

public class TradeBin
{
    public DateTime Timestamp { get; set; }
    public string Symbol { get; set; }
    public double? Open { get; set; }
    public double? High { get; set; }
    public double? Low { get; set; }
    public double? Close { get; set; }
    public long Trades { get; set; }
    public long Volume { get; set; }
    public double? Vwap { get; set; }
    public long? LastSize { get; set; }
    public long Turnover { get; set; }
    public double? HomeNotional { get; set; }
    public double? ForeignNotional { get; set; }
}