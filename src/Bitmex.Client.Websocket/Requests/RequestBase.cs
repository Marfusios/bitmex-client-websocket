using Bitmex.Client.Websocket.Messages;
using Newtonsoft.Json;

namespace Bitmex.Client.Websocket.Requests
{
    public abstract class RequestBase : MessageBase
    {
        public override MessageType Op
        {
            get => Operation;
            set { }
        }

        [JsonIgnore]
        public abstract MessageType Operation { get; }
    }
}