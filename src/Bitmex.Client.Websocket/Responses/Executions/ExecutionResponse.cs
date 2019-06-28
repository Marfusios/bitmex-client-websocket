using Bitmex.Client.Websocket.Json;
using Bitmex.Client.Websocket.Messages;
using Newtonsoft.Json.Linq;
using System.Reactive.Subjects;

namespace Bitmex.Client.Websocket.Responses.Executions
{
    public class ExecutionResponse : ResponseBase
    {
        public override MessageType Op => MessageType.Execution;

        public Execution[] Data { get; set; }

        internal static bool TryHandle(JObject response, ISubject<ExecutionResponse> subject)
        {
            if (response?["table"]?.Value<string>() != "execution")
                return false;

            var parsed = response.ToObject<ExecutionResponse>(BitmexJsonSerializer.Serializer);
            subject.OnNext(parsed);

            return true;
        }
    }
}
