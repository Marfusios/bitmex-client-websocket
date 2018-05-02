using System.Reactive.Subjects;
using Bitmex.Client.Websocket.Json;
using Bitmex.Client.Websocket.Messages;
using Newtonsoft.Json.Linq;

namespace Bitmex.Client.Websocket.Responses
{
    public class AuthenticationResponse : MessageBase
    {
        public override MessageType Op => MessageType.AuthKey;

        public bool Success { get; set; }

        internal static bool TryHandle(JObject response, ISubject<AuthenticationResponse> subject)
        {
            if (response?["request"]?["op"]?.Value<string>() == "authKey")
            {
                var parsed = response.ToObject<AuthenticationResponse>(BitmexJsonSerializer.Serializer);
                subject.OnNext(parsed);
                return true;
            }
            return false;
        }
    }
}
