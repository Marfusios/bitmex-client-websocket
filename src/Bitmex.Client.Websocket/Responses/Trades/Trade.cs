using System;
using System.Runtime.Serialization;

namespace Bitmex.Client.Websocket.Responses.Trades;

public class Trade
{
    public DateTime Timestamp { get; set; }
    public string Symbol {get; set; }
    public BitmexSide Side { get; set; }
    public long Size {get; set; }
    public double Price { get; set; }
    public BitmexTickDirection TickDirection { get; set; }

    [DataMember(Name = "trdMatchID")]
    public string TrdMatchId { get; set; }
    public long? GrossValue { get; set; }
    public double? HomeNotional { get; set; }
    public double? ForeignNotional { get; set; }
}