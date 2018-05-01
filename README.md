# Bitmex websocket API client [![Build Status](https://travis-ci.org/Marfusios/bitmex-client-websocket.svg?branch=master)](https://travis-ci.org/Marfusios/bitmex-client-websocket) [![NuGet version](https://badge.fury.io/nu/Bitmex.Client.Websocket.svg)](https://badge.fury.io/nu/Bitmex.Client.Websocket)

This is a C# implementation of the Bitmex websocket API found here:

https://www.bitmex.com/app/wsAPI

### License: 
    Apache License 2.0

### Features

* instalation via NuGet ([Bitmex.Client.Websocket](https://www.nuget.org/packages/Bitmex.Client.Websocket))
* public and authenticated API
* targeting .NET Standard 2.0 (.NET Core, Linux/MacOS compatible)
* reactive extensions ([Rx.NET](https://github.com/Reactive-Extensions/Rx.NET))
* integrated logging ([Serilog](https://serilog.net/))

### Usage

```csharp
var exitEvent = new ManualResetEvent(false);
var url = Bitmex.ApiWebsocketUrl;

using (var communicator = new BitmexWebsocketCommunicator(url))
{
    using (var client = new BitmexWebsocketClient(communicator))
    {
        client.Streams.PongStream.Subscribe(pong =>
        {
            Console.WriteLine($"Pong received! Id: {pong.Cid}") // Pong received! Id: 123456
            exitEvent.Set();
        });

        await communicator.Start();
        await client.Send(new PingRequest() {Cid = 123456});

        exitEvent.WaitOne(TimeSpan.FromSeconds(30));
    }
}
```

More usage examples:
* integration tests ([link](test_integration/Bitmex.Client.Websocket.Tests.Integration))
* console sample ([link](test_integration/Bitmex.Client.Websocket.Sample/Program.cs))

### API coverage

| PUBLIC                 |    Covered     |  
|------------------------|:--------------:|
| Info                   |  ✔            |
| Ping-Pong              |               |
| Errors                 |               |
| Ticker                 |               |
| Trades                 |  ✔            |
| Books                  |  ✔            |

| AUTHENTICATED          |    Covered     |  
|------------------------|:--------------:|
| Account info           |  ✔            |
| Orders                 |  ✔            |
| Positions              |                |
| Trades                 |                |
| Funding                |                |
| Wallets                |  ✔            |
| Balance                |                |
| Notifications          |                |

**Pull Requests are welcome!**

Donations gratefully accepted.
* Bitcoin: 1HfxKZhvm68qK3gE8bJAdDBWkcZ2AFs9pw
* Litecoin: LftdENE8DTbLpV6RZLKLdzYzVU82E6dz4W
* Ethereum: 0xb9637c56b307f24372cdcebd208c0679d4e48a47