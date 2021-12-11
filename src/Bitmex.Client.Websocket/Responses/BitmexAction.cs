using System.Runtime.Serialization;

namespace Bitmex.Client.Websocket.Responses;

public enum BitmexAction
{
    Undefined,

    [DataMember(Name = "partial")]
    Partial,

    [DataMember(Name = "insert")]
    Insert,

    [DataMember(Name = "update")]
    Update,

    [DataMember(Name = "delete")]
    Delete
}