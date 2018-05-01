using Bitmex.Client.Websocket.Messages;
using Newtonsoft.Json;

namespace Bitmex.Client.Websocket.Responses
{
    public class AuthenticationResponse : MessageBase
    {
        public string Status { get; set; }
        public int ChanId { get; set; }
        public int UserId { get; set; }
        public string Code { get; set; }
        public object Caps { get; set; }

        [JsonIgnore] 
        public bool IsAuthenticated => Status == "OK";
    }
}
