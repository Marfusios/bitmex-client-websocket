using System.Runtime.Serialization;
using Bitmex.Client.Websocket.Messages;

namespace Bitmex.Client.Websocket.Requests;

/// <summary>
/// Base class for every subscribe and unsubscribe request
/// </summary>
public abstract class SubscribeRequestBase : RequestBase
{
    /// <inheritdoc />
    [IgnoreDataMember]
    public override MessageType Operation => !IsUnsubscribe ? MessageType.Subscribe : MessageType.Unsubscribe;

    /// <summary>
    /// Subscription arguments array which is serialized
    /// </summary>
    public string[] Args => new[]
    {
        string.IsNullOrWhiteSpace(Symbol) ? Topic : $"{Topic}:{Symbol}"
    };

    /// <summary>
    /// Target subscription topic
    /// </summary>
    [IgnoreDataMember]
    public abstract string Topic { get; }

    /// <summary>
    /// Target subscription pair (could be null, then it will subscribe to everything)
    /// </summary>
    [IgnoreDataMember]
    public virtual string Symbol { get; } = string.Empty;

    /// <summary>
    /// Set true if you want to unsubscribe from the stream
    /// </summary>
    [IgnoreDataMember]
    public bool IsUnsubscribe { get; set; }
}