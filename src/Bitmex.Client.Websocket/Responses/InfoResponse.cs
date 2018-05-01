using System;
using System.Collections.Generic;
using System.Reactive.Subjects;
using Bitmex.Client.Websocket.Json;
using Bitmex.Client.Websocket.Messages;
using Newtonsoft.Json.Linq;

namespace Bitmex.Client.Websocket.Responses
{
    public class InfoResponse : MessageBase
    {
        public override MessageType Op => MessageType.Info;

        public string Info { get; set; }
        public DateTime Version { get; set; }
        public DateTime Timestamp { get; set; }
        public string Docs { get; set; }
        public Dictionary<string, object> Limit { get; set; }

        internal static bool TryHandle(JObject response, ISubject<InfoResponse> subject)
        {
            if (response?["info"] != null && response?["version"] != null)
            {
                var parsed = response.ToObject<InfoResponse>(BitmexJsonSerializer.Serializer);
                subject.OnNext(parsed);
                return true;
            }
            return false;
        }
    }
}
