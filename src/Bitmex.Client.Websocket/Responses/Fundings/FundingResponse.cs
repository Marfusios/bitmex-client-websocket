using System.Reactive.Subjects;
using Bitmex.Client.Websocket.Json;
using Bitmex.Client.Websocket.Messages;

namespace Bitmex.Client.Websocket.Responses.Fundings;

/// <summary>
/// Fundings response
/// </summary>
public class FundingResponse : ResponseBase
{
    /// <summary>
    /// Operation type
    /// </summary>
    public override MessageType Op => MessageType.Funding;

    /// <summary>
    /// All latest fundings
    /// </summary>
    public Funding[] Data { get; set; }

    internal static bool TryHandle(string response, ISubject<FundingResponse> subject)
    {
        if (!BitmexJsonSerializer.ContainsValue(response, "funding"))
            return false;

        var parsed = BitmexJsonSerializer.Deserialize<FundingResponse>(response);
        subject.OnNext(parsed);

        return true;
    }
}