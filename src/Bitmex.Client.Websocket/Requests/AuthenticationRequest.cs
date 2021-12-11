using System;
using System.Runtime.Serialization;
using Bitmex.Client.Websocket.Messages;
using Bitmex.Client.Websocket.Utils;

namespace Bitmex.Client.Websocket.Requests;

public class AuthenticationRequest : RequestBase
{
    readonly string _apiKey;
    readonly string _authSig;
    readonly long _authNonce;
    readonly string _authPayload;

    public AuthenticationRequest(string apiKey, string apiSecret)
    {
        _apiKey = apiKey ?? throw new ArgumentNullException(nameof(apiKey));

        _authNonce = BitmexAuthentication.CreateAuthNonce();
        _authPayload = BitmexAuthentication.CreateAuthPayload(_authNonce);

        _authSig = BitmexAuthentication.CreateSignature(apiSecret ?? throw new ArgumentNullException(nameof(apiSecret)), _authPayload);
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