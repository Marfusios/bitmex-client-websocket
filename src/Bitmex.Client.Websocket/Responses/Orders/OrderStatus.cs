using System.Runtime.Serialization;

namespace Bitmex.Client.Websocket.Responses.Orders;

public enum OrderStatus
{
    Undefined,

    [DataMember(Name = "new")]
    New,

    [DataMember(Name = "filled")]
    Filled,

    [DataMember(Name = "partiallyFilled")]
    PartiallyFilled,

    [DataMember(Name = "canceled")]
    Canceled,

    [DataMember(Name = "rejected")]
    Rejected
}