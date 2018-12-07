using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Bitmex.Client.Websocket.Client;
using Bitmex.Client.Websocket.Files;
using Bitmex.Client.Websocket.Responses.Trades;
using Xunit;

namespace Bitmex.Client.Websocket.Tests.Integration
{
    public class BitmexFileCommunicatorTests
    {
        // ----------------------------------------------------------------
        // Don't forget to decompress gzip files before starting the tests
        // ----------------------------------------------------------------

        [SkippableFact]
        public async Task OnStart_ShouldStreamMessagesFromFile()
        {
            var files = new[]
            {
                "data/bitmex_raw_xbtusd_2018-11-13.txt"
            };
            foreach (var file in files)
            {
                var exist = File.Exists(file);
                Skip.If(!exist, $"The file '{file}' doesn't exist. Don't forget to decompress gzip file!");
            }

            var trades = new List<Trade>();

            var communicator = new BitmexFileCommunicator();
            communicator.FileNames = files;
            communicator.Delimiter = ";;";

            var client = new BitmexWebsocketClient(communicator);
            client.Streams.TradesStream.Subscribe(response =>
            {
                trades.AddRange(response.Data);
            });

            await communicator.Start();

            Assert.Equal(44259, trades.Count);
        }
    }
}
