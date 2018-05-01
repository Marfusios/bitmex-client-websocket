using Bitmex.Client.Websocket.Messages;

namespace Bitmex.Client.Websocket.Responses
{
    public class ErrorResponse : MessageBase
    {
        public string Code { get; set; }
        public string Msg { get; set; }
    }
}
