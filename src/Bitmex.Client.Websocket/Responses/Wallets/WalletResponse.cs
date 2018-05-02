using System.Reactive.Subjects;
using Bitmex.Client.Websocket.Json;
using Bitmex.Client.Websocket.Messages;
using Newtonsoft.Json.Linq;

namespace Bitmex.Client.Websocket.Responses.Wallets
{
    public class WalletResponse : ResponseBase
    {
        public override MessageType Op => MessageType.Wallet;

        public Wallet[] Data { get; set; }

        internal static bool TryHandle(JObject response, ISubject<WalletResponse> subject)
        {
            if (response?["table"]?.Value<string>() != "wallet")
                return false;

            var parsed = response.ToObject<WalletResponse>(BitmexJsonSerializer.Serializer);
            subject.OnNext(parsed);

            return true;
        }
    }
}
