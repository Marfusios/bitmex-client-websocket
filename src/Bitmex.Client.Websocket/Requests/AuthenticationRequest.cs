using System.Runtime.Serialization;
using Bitmex.Client.Websocket.Messages;
using Bitmex.Client.Websocket.Utils;
using Bitmex.Client.Websocket.Validations;

namespace Bitmex.Client.Websocket.Requests
{
    public class AuthenticationRequest : RequestBase
    {
        private readonly string _apiKey;
        private readonly string _authSig;
        private readonly long _authNonce;
        private readonly string _authPayload;

        public AuthenticationRequest(string apiKey, string apiSecret)
        {
            BmxValidations.ValidateInput(apiKey, nameof(apiKey));
            BmxValidations.ValidateInput(apiSecret, nameof(apiSecret));

            _apiKey = apiKey;

            _authNonce = BitmexAuthentication.CreateAuthNonce();
            _authPayload = BitmexAuthentication.CreateAuthPayload(_authNonce);

            _authSig = BitmexAuthentication.CreateSignature(apiSecret, _authPayload);
        }

        [IgnoreDataMember]
        public override MessageType Operation => MessageType.AuthKey;

        public object[] Args => new object[]
        {
            _apiKey,
            _authNonce,
            _authSig
        };
    }
}
