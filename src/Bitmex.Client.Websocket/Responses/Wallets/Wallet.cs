using System;
using System.Diagnostics;
using Bitmex.Client.Websocket.Utils;

namespace Bitmex.Client.Websocket.Responses.Wallets
{
    [DebuggerDisplay("Wallet: {Currency} - {Balance}")]
    public class Wallet
    {
        public long Account { get; set; }
        public string Currency { get; set; }

        public long? PrevDeposited { get; set; }
        public long? PrevWithdrawn { get; set; }
        public long? PrevTransferIn { get; set; }
        public long? PrevTransferOut { get; set; }
        public long? PrevAmount { get; set; }
        public long? TransferIn { get; set; }
        public long? TransferOut { get; set; }
        public long? Amount { get; set; }
        public long? PendingCredit { get; set; }
        public long? PendingDebit { get; set; }
        public long? ConfirmedDebit { get; set; }

        public DateTime Timestamp { get; set; }
        public string Addr { get; set; }
        public string Script { get; set; }
        public string[] WithdrawalLock {get; set; }

        public double Balance => BitmexConverter.ConvertToBtc(Currency, Amount ?? 0);
    }
}
