using System.Diagnostics;
using Newtonsoft.Json;

namespace Bitmex.Client.Websocket.Responses.Wallets
{
    [DebuggerDisplay("Wallet: {Currency} - {Balance}")]
    [JsonConverter(typeof(WalletConverter))]
    public class Wallet
    {
        public WalletType Type { get; set; }
        public string Currency { get; set; }
        public double Balance { get; set; }
        public double UnsettledInterest { get; set; }
        public double? BalanceAvailable { get; set; }
    }
}
