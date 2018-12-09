using System;

namespace Bitmex.Client.Websocket.Sample.WinForms
{
    public interface IStatsView
    {
        Action OnInit { get; set; }
        Action OnStart { set; }
        Action OnStop { set; }

        string Bid { get; set; }
        string Ask { get; set; }

        string BidAmount { get; set; }
        string AskAmount { get; set; }

        void Trades1Min(string value, Side side);
        void Trades5Min(string value, Side side);
        void Trades15Min(string value, Side side);

        string Ping { get; set; }
    }

    public enum Side
    {
        Buy,
        Sell
    }
}
