using Bitmex.Client.Websocket.Json;
using Bitmex.Client.Websocket.Messages;
using System.Reactive.Subjects;

namespace Bitmex.Client.Websocket.Responses.Executions
{
    public class ExecutionResponse : ResponseBase
    {
        public override MessageType Op => MessageType.Execution;

        public Execution[] Data { get; set; }

        internal static bool TryHandle(string response, ISubject<ExecutionResponse> subject)
        {
            if (!BitmexJsonSerializer.ContainsValue(response, "execution"))
                return false;

            var parsed = BitmexJsonSerializer.Deserialize<ExecutionResponse>(response);
            subject.OnNext(parsed);

            return true;
        }
    }
}
