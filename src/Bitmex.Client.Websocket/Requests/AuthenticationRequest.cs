using Bitmex.Client.Websocket.Messages;
using Bitmex.Client.Websocket.Utils;
using Bitmex.Client.Websocket.Validations;

namespace Bitmex.Client.Websocket.Requests
{
    public class AuthenticationRequest : RequestBase
    {
        public AuthenticationRequest(string apiKey, string apiSecret)
        {
            BmxValidations.ValidateInput(apiKey, nameof(apiKey));
            BmxValidations.ValidateInput(apiSecret, nameof(apiSecret));

            ApiKey = apiKey;

            AuthNonce = BitmexAuthentication.CreateAuthNonce();
            AuthPayload = BitmexAuthentication.CreateAuthPayload(AuthNonce);

            AuthSig = BitmexAuthentication.CreateSignature(AuthPayload, apiSecret);
        }

        public override MessageType Operation => MessageType.Auth;

        public string ApiKey { get; }
        public string AuthSig { get; }
        public long AuthNonce { get; }
        public string AuthPayload { get; }


        
    }
}
