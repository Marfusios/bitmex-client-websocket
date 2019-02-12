using System;
using System.Diagnostics;
using Bitmex.Client.Websocket.Utils;

namespace Bitmex.Client.Websocket.Responses.Wallets
{
    /// <summary>
    /// Information about your wallet (balance, changes, address, etc)
    /// </summary>
    [DebuggerDisplay("Wallet: {Currency} - {BalanceBtc}")]
    public class Wallet
    {
        /// <summary>
        /// Account identification
        /// </summary>
        public long Account { get; set; }

        /// <summary>
        /// Current `Amount` currency, for example `XBt` which is satoshi
        /// </summary>
        public string Currency { get; set; }

        public long? PrevDeposited { get; set; }
        public long? PrevWithdrawn { get; set; }
        public long? PrevTransferIn { get; set; }
        public long? PrevTransferOut { get; set; }
        public long? PrevAmount { get; set; }

        public long? TransferIn { get; set; }
        public long? TransferOut { get; set; }

        /// <summary>
        /// Current balance in satoshis (call `BalanceBtc` property to get BTC representation)
        /// </summary>
        public long? Amount { get; set; }


        public long? PendingCredit { get; set; }
        public long? PendingDebit { get; set; }
        public long? ConfirmedDebit { get; set; }

        public DateTime Timestamp { get; set; }
        public string Addr { get; set; }
        public string Script { get; set; }
        public string[] WithdrawalLock {get; set; }

        /// <summary>
        /// Converted satoshis 'Amount' into double BTC representation
        /// </summary>
        public double BalanceBtc => BitmexConverter.ConvertToBtc(Currency, Amount ?? 0);

        /// <summary>
        /// Converted satoshis 'Amount' into decimal BTC representation
        /// </summary>
        public decimal BalanceBtcDecimal => BitmexConverter.ConvertToBtcDecimal(Currency, Amount ?? 0);
    }
}
