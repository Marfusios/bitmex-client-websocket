using System.Collections.Generic;
using Bitmex.Client.Websocket.Messages;

namespace Bitmex.Client.Websocket.Responses;

/// <summary>
/// Base message for every response
/// </summary>
public class ResponseBase : MessageBase
{
    /// <summary>
    /// The type of the message. Types:
    /// 'partial'; This is a table image, replace your data entirely.
    /// 'update': Update a single row.
    /// 'insert': Insert a new row.
    /// 'delete': Delete a row.
    /// </summary>
    public BitmexAction Action { get; set; }

    /// <summary>
    /// Table name / Subscription topic.
    /// Could be "trade", "order", "instrument", etc.
    /// </summary>
    public string Table { get; set; }

    /// <summary>
    /// Attribute names that are guaranteed to be unique per object.
    /// If more than one is provided, the key is composite.
    /// Use these key names to uniquely identify rows. Key columns are guaranteed
    /// to be present on all data received.
    /// </summary>
    public string[] Keys { get; set; }

    /// <summary>
    /// This lists the shape of the table. The possible types:
    /// "symbol" - In most languages this is equal to "string"
    /// "guid"
    /// "timestamp"
    /// "timespan"
    /// "float"
    /// "long"
    /// "integer"
    /// "boolean"
    /// </summary>
    public Dictionary<string, string> Types { get; set; }

    /// <summary>
    /// This lists key relationships with other tables.
    /// For example, `quote`'s foreign key is {"symbol": "instrument"}
    /// </summary>
    public Dictionary<string, string> ForeignKeys { get; set; }


    /// <summary>
    /// These are internal fields that indicate how responses are sorted and grouped.
    /// </summary>
    public Dictionary<string, string> Attributes { get; set; }

    /// <summary>
    /// When multiple subscriptions are active to the same table, use the `filter` to correlate which datagram
    /// belongs to which subscription, as the `table` property will not contain the subscription's symbol.
    /// </summary>
    public FilterInfo Filter { get; set; }
}