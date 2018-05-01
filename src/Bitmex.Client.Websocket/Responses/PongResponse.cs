using System;
using Bitmex.Client.Websocket.Json;
using Bitmex.Client.Websocket.Messages;
using Newtonsoft.Json;

namespace Bitmex.Client.Websocket.Responses
{
    public class PongResponse : MessageBase
    {
        public int Cid { get; set; }

        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime Ts { get; set; }
    }
}
