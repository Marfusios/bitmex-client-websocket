using Bitmex.Client.Websocket.Messages;
using Newtonsoft.Json;

namespace Bitmex.Client.Websocket.Requests
{
    /// <summary>
    /// Base class for every request
    /// </summary>
    public abstract class RequestBase : MessageBase
    {
        /// <inheritdoc />
        public override MessageType Op
        {
            get => Operation;
            set { }
        }

        /// <summary>
        /// Same as 'Op' property, but more readable and overriden by descendants
        /// </summary>
        [JsonIgnore]
        public abstract MessageType Operation { get; }

        /// <summary>
        /// Operation as string for Raw requests (for example: ping)
        /// </summary>
        [JsonIgnore]
        public virtual string OperationString => Operation.ToString().ToLower();

        /// <summary>
        /// If is set to true, whole request is not serialized as JSON but only 'OperationString' is used
        /// </summary>
        [JsonIgnore] 
        public virtual bool IsRaw { get; } = false;
    }
}