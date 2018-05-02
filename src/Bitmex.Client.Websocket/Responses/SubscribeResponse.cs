using System.Reactive.Subjects;
using Bitmex.Client.Websocket.Json;
using Bitmex.Client.Websocket.Messages;
using Newtonsoft.Json.Linq;

namespace Bitmex.Client.Websocket.Responses
{
    public class SubscribeResponse : MessageBase
    {
        public override MessageType Op => MessageType.Subscribe;

        public string Subscribe { get; set; }
        public bool Success { get; set; }

        internal static bool TryHandle(JObject response, ISubject<SubscribeResponse> subject)
        {
            if (response?["subscribe"] != null)
            {
                var parsed = response.ToObject<SubscribeResponse>(BitmexJsonSerializer.Serializer);
                subject.OnNext(parsed);
                return true;
            }
            return false;
        }
    }
}
