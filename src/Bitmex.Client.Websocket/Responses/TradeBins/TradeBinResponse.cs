using System.Reactive.Subjects;
using System.Runtime.Serialization;
using Bitmex.Client.Websocket.Json;
using Bitmex.Client.Websocket.Messages;

namespace Bitmex.Client.Websocket.Responses.TradeBins;

/// <summary>
/// Trades bin response, contains all trades executed in a selected time range
/// </summary>
public class TradeBinResponse : ResponseBase
{
    /// <summary>
    /// Operation type
    /// </summary>
    public override MessageType Op => MessageType.Trade;

    /// <summary>
    /// Trades data
    /// </summary>
    public TradeBin[] Data { get; set; }

    /// <summary>
    /// Size of the bin ('1min', '5min', '1h', etc)
    /// </summary>
    [IgnoreDataMember]
    public string Size { get; private set; }


    internal static bool TryHandle(string response, ISubject<TradeBinResponse> subject)
    {
        if (!BitmexJsonSerializer.ContainsRaw(response, "tradeBin"))
            return false;

        var parsed = BitmexJsonSerializer.Deserialize<TradeBinResponse>(response);
        parsed.Size = parsed.Table?.Replace("tradeBin", string.Empty).Trim();
        subject.OnNext(parsed);

        return true;
    }
}