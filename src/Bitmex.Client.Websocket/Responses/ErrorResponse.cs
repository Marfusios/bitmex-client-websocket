using System.Collections.Generic;
using System.Reactive.Subjects;
using Bitmex.Client.Websocket.Json;
using Bitmex.Client.Websocket.Messages;
using Newtonsoft.Json.Linq;

namespace Bitmex.Client.Websocket.Responses
{
    public class ErrorResponse : MessageBase
    {
        public override MessageType Op => MessageType.Error;

        public double? Status { get; set; }
        public string Error { get; set; }
        public Dictionary<string, object> Meta { get; set; }
        public Dictionary<string, object> Request { get; set; }

        internal static bool TryHandle(JObject response, ISubject<ErrorResponse> subject)
        {
            if (response?["error"] != null)
            {
                var parsed = response.ToObject<ErrorResponse>(BitmexJsonSerializer.Serializer);
                subject.OnNext(parsed);
                return true;
            }
            return false;
        }
    }
}
