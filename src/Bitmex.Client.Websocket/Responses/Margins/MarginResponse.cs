using System.Reactive.Subjects;
using Bitmex.Client.Websocket.Json;
using Bitmex.Client.Websocket.Messages;
using Newtonsoft.Json.Linq;

namespace Bitmex.Client.Websocket.Responses.Margins
{
    public class MarginResponse : ResponseBase
    {
        public override MessageType Op => MessageType.Margin;

        public Margin[] Data { get; set; }

        internal static bool TryHandle(JObject response, ISubject<MarginResponse> subject)
        {
            if (response?["table"]?.Value<string>() != "margin")
                return false;

            var parsed = response.ToObject<MarginResponse>(BitmexJsonSerializer.Serializer);
            subject.OnNext(parsed);

            return true;
        }
    }
}
