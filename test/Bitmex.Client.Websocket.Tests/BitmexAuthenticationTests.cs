using Bitmex.Client.Websocket.Utils;
using Xunit;

namespace Bitmex.Client.Websocket.Tests;

public class BitmexAuthenticationTests
{
    [Fact]
    public void CreateSignature_ShouldReturnCorrectString()
    {
        var nonce = BitmexAuthentication.CreateAuthNonce(123456);
        var payload = BitmexAuthentication.CreateAuthPayload(nonce);
        var signature = BitmexAuthentication.CreateSignature(payload, "api_secret");

        Assert.Equal("7657aa8b00b54ee7d58ed0ed42b6cad6d8b1e008bee4617b70d11cd87dbbc1e6", signature);
    }
}