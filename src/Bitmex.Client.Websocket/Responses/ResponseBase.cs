using System.Collections.Generic;
using Bitmex.Client.Websocket.Messages;

namespace Bitmex.Client.Websocket.Responses
{
    public class ResponseBase : MessageBase
    {
        public BitmexAction Action { get; set; }

        public string Table { get; set; }
        public string[] Keys { get; set; }
        public Dictionary<string, string> Types { get; set; }
        public Dictionary<string, string> ForeignKeys { get; set; }
        public Dictionary<string, string> Attributes { get; set; }
    }
}
