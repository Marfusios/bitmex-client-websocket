using System.Runtime.Serialization;

namespace Bitmex.Client.Websocket.Responses.Instruments
{
    public enum InstrumentMarkMethod
    {
        Undefined,

        [DataMember(Name = "fairPrice")]
        FairPrice,

        [DataMember(Name = "lastPrice")]
        LastPrice
    }
}
