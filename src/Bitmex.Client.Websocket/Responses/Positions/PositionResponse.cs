using System.Reactive.Subjects;
using Bitmex.Client.Websocket.Json;
using Bitmex.Client.Websocket.Messages;
using Newtonsoft.Json.Linq;

namespace Bitmex.Client.Websocket.Responses.Positions
{
    public class PositionResponse : ResponseBase
    {
        public override MessageType Op => MessageType.Position;

        public Position[] Data { get; set; }

        internal static bool TryHandle(JObject response, ISubject<PositionResponse> subject)
        {
            if (response?["table"]?.Value<string>() != "position")
                return false;

            var parsed = response.ToObject<PositionResponse>(BitmexJsonSerializer.Serializer);
            subject.OnNext(parsed);

            return true;
        }
    }
}
