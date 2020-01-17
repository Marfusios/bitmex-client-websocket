using System.Runtime.Serialization;

namespace Bitmex.Client.Websocket.Responses.Instruments
{
    public enum InstrumentState
    {
        Undefined,

        [DataMember(Name = "open")]
        Open,

        [DataMember(Name = "unlisted")]
        Unlisted,

        [DataMember(Name = "closed")]
        Closed
    }
}
