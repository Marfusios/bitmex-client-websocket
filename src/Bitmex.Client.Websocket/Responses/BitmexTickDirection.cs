using System.Runtime.Serialization;

namespace Bitmex.Client.Websocket.Responses
{
    public enum BitmexTickDirection
    {
        Undefined,

        [DataMember(Name = "minusTick")]
        MinusTick,

        [DataMember(Name = "plusTick")]
        PlusTick,

        [DataMember(Name = "zeroMinusTick")]
        ZeroMinusTick,

        [DataMember(Name = "zeroPlusTick")]
        ZeroPlusTick
    }
}
