using System.Runtime.Serialization;
using Bitmex.Client.Websocket.Messages;

namespace Bitmex.Client.Websocket.Requests;

/// <summary>
/// Base class for every request
/// </summary>
public abstract class RequestBase : MessageBase
{
    /// <inheritdoc />
    public override MessageType Op
    {
        get => Operation;
        set { }
    }

    /// <summary>
    /// Same as 'Op' property, but more readable and overriden by descendants
    /// </summary>
    [IgnoreDataMember]
    public abstract MessageType Operation { get; }

    /// <summary>
    /// Operation as string for Raw requests (for example: ping)
    /// </summary>
    [IgnoreDataMember]
    public virtual string OperationString => Operation.ToString().ToLower();

    /// <summary>
    /// If is set to true, whole request is not serialized as JSON but only 'OperationString' is used
    /// </summary>
    [IgnoreDataMember] 
    public virtual bool IsRaw { get; } = false;
}