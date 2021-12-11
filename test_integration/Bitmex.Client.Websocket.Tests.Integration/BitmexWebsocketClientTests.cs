using System;
using System.Threading;
using System.Threading.Tasks;
using Bitmex.Client.Websocket.Client;
using Bitmex.Client.Websocket.Requests;
using Bitmex.Client.Websocket.Responses;
using Microsoft.Extensions.Logging.Abstractions;
using Websocket.Client;
using Xunit;

namespace Bitmex.Client.Websocket.Tests.Integration;

public class BitmexWebsocketClientTests
{
    static readonly string API_KEY = "your_api_key";
    static readonly string API_SECRET = "";

    [Fact]
    public async Task PingPong()
    {
        var url = BitmexValues.ApiWebsocketUrl;
        using var apiClient = new WebsocketClient(url);
        PongResponse received = null;
        var receivedEvent = new ManualResetEvent(false);

        using var client = new BitmexWebsocketClient(NullLogger.Instance, apiClient);
        client.Streams.PongStream.Subscribe(pong =>
        {
            received = pong;
            receivedEvent.Set();
        });

        await apiClient.Start();

        client.Send(new PingRequest());

        receivedEvent.WaitOne(TimeSpan.FromSeconds(30));

        Assert.NotNull(received);
    }

    [SkippableFact]
    public async Task Authentication()
    {
        Skip.If(string.IsNullOrWhiteSpace(API_SECRET));

        var url = BitmexValues.ApiWebsocketUrl;
        using var apiClient = new WebsocketClient(url);
        AuthenticationResponse received = null;
        var receivedEvent = new ManualResetEvent(false);

        using var client = new BitmexWebsocketClient(NullLogger.Instance, apiClient);
        client.Streams.AuthenticationStream.Subscribe(auth =>
        {
            received = auth;
            receivedEvent.Set();
        });

        await apiClient.Start();

        client.Send(new AuthenticationRequest(API_KEY, API_SECRET));

        receivedEvent.WaitOne(TimeSpan.FromSeconds(30));

        Assert.NotNull(received);
        Assert.True(received.Success);
    }

}