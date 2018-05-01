using Bitmex.Client.Websocket.Messages;
using Newtonsoft.Json;

namespace Bitmex.Client.Websocket.Requests
{
    public abstract class SubscribeRequestBase : RequestBase
    {
        public override MessageType Operation => MessageType.Subscribe;
        public string[] Args => new[] {Topic};

        [JsonIgnore]
        public abstract string Topic { get; }
    }
}
