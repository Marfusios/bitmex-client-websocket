using System;
using System.Reactive.Subjects;
using Bitmex.Client.Websocket.Json;
using Bitmex.Client.Websocket.Messages;
using Newtonsoft.Json.Linq;

namespace Bitmex.Client.Websocket.Responses.Instruments
{
    public class InstrumentResponse : ResponseBase
    {
        public override MessageType Op => MessageType.Instrument;

        public Instrument[] Data { get; set; }

        internal static bool TryHandle(JObject response, ISubject<InstrumentResponse> subject)
        {
            if (response?["table"]?.Value<string>() != "instrument")
                return false;

            var parsed = response.ToObject<InstrumentResponse>(BitmexJsonSerializer.Serializer);
            subject.OnNext(parsed);

            return true;
        }
    }
}
