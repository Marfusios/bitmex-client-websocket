namespace Bitmex.Client.Websocket.Tests
{
    using System;
    using System.Collections.Generic;
    using Json;
    using Messages;
    using Requests;
    using Utils;
    using Xunit;
    using Xunit.Abstractions;

    public class BitmexChannelServiceTests
    {
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly BitmexChannelService bitmexChannelService = new BitmexChannelService();

        public BitmexChannelServiceTests(ITestOutputHelper testOutputHelper)
        {
            this._testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void Send_Payload_ShouldReturnCorrectString()
        {
            var channel = this.bitmexChannelService.CreateChannel("TestChannel");
            var message = new MultiplexingMessageBase
            {
                MessageType = MultiplexingMessageType.Message,
                Payload = new AuthenticationRequest("apiKey", "apiSecret")
            };

            var result = bitmexChannelService.Send(channel, message);
            _testOutputHelper.WriteLine(result);
            Assert.EndsWith("\"op\":\"authKey\"}]", result);
        }

        [Fact]
        public void Send_NoPayload_ShouldReturnCorrectString()
        {
            var channel = this.bitmexChannelService.CreateChannel("TestChannel");
            var message = new MultiplexingMessageBase
            {
                MessageType = MultiplexingMessageType.Message
            };


            var result = bitmexChannelService.Send(channel, message);
            _testOutputHelper.WriteLine(result);
            Assert.EndsWith("\"TestChannel\"]", result);
        }

        [Fact]
        public void Test()
        {
            var items = new List<object> { 0, Guid.NewGuid(), "TestChannel" };
            this._testOutputHelper.WriteLine(BitmexJsonSerializer.Serialize(items));
        }
    }
}
