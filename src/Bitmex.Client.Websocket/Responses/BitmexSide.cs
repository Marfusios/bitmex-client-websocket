using System.Runtime.Serialization;

namespace Bitmex.Client.Websocket.Responses
{
    public enum BitmexSide
    {
        [DataMember(Name = "")]
        Undefined,

        [DataMember(Name = "buy")]
        Buy,

        [DataMember(Name = "sell")]
        Sell
    }
}
