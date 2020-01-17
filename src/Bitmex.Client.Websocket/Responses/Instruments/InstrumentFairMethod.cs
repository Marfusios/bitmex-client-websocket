using System.Runtime.Serialization;

namespace Bitmex.Client.Websocket.Responses.Instruments
{
    public enum InstrumentFairMethod
    {
        Undefined,

        [DataMember(Name = "impactMidPrice")]
        ImpactMidPrice,

        [DataMember(Name = "fundingRate")]
        FundingRate
    }
}
