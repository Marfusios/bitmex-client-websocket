using System.Reactive.Subjects;
using System;
using Bitmex.Client.Websocket.Messages;

namespace Bitmex.Client.Websocket.Responses
{
    public class PongResponse : MessageBase
    {
        public override MessageType Op => MessageType.Ping;

        public string Message { get; set; }

        internal static bool TryHandle(string response, ISubject<PongResponse> subject)
        {
            if (response == null)
                return false;

            if (response.IndexOf("pong", StringComparison.OrdinalIgnoreCase) < 0)
                return false;

            var parsed = new PongResponse {Message = response};
            subject.OnNext(parsed);
            return true;
        }
    }
}
