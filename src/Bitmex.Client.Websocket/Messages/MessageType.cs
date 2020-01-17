using System.Runtime.Serialization;

namespace Bitmex.Client.Websocket.Messages
{
    public enum MessageType
    {
        // Do not rename, used in requests
        [DataMember(Name = "ping")]
        Ping,
        [DataMember(Name = "authKey")]
        AuthKey,
        [DataMember(Name = "subscribe")]
        Subscribe,
        [DataMember(Name = "unsubscribe")]
        Unsubscribe,
        [DataMember(Name = "cancelAllAfter")]
        CancelAllAfter,

        // Can be renamed, only for responses
        Error,
        Info,
        Trade,
        OrderBook,
        Wallet,
        Order,
        Position,
        Quote,
        Instrument,
        Margin,
        Execution,
        Funding
    }
}
