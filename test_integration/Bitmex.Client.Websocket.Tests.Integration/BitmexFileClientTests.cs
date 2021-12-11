using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Bitmex.Client.Websocket.Client;
using Bitmex.Client.Websocket.Files;
using Bitmex.Client.Websocket.Responses.Trades;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;

namespace Bitmex.Client.Websocket.Tests.Integration;

public class BitmexFileClientTests
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

        var fileClient = new BitmexFileClient
        {
            FileNames = files,
            Delimiter = ";;"
        };

        var client = new BitmexWebsocketClient(NullLogger.Instance, fileClient);
        client.Streams.TradesStream.Subscribe(response =>
        {
            trades.AddRange(response.Data);
        });

        await fileClient.Start();

        Assert.Equal(44259, trades.Count);
    }
}