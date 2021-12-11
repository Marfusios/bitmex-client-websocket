using System.Reactive.Subjects;
using Bitmex.Client.Websocket.Json;
using Bitmex.Client.Websocket.Messages;

namespace Bitmex.Client.Websocket.Responses.Wallets;

public class WalletResponse : ResponseBase
{
    public override MessageType Op => MessageType.Wallet;

    public Wallet[] Data { get; set; }

    internal static bool TryHandle(string response, ISubject<WalletResponse> subject)
    {
        if (!BitmexJsonSerializer.ContainsValue(response, "wallet"))
            return false;

        var parsed = BitmexJsonSerializer.Deserialize<WalletResponse>(response);
        subject.OnNext(parsed);

        return true;
    }
}