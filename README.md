![Logo](bitmex-logo-alt.png)
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
        client.Streams.InfoStream.Subscribe(info =>
        {
            Console.WriteLine($"Info received, reconnection happened.")
            client.Send(new PingRequest()).Wait();
        });

        client.Streams.PongStream.Subscribe(pong =>
        {
            Console.WriteLine($"Pong received!")
            exitEvent.Set();
        });

        await communicator.Start();

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
| Ping-Pong              |  ✔            |
| Errors                 |  ✔            |
| Subscribe              |  ✔            |
| Unsubscribe            |                |
| Announcement           |                |
| Chat                   |                |
| Connected              |                |
| Funding                |                |
| Instrument             |                |
| Insurance              |                |
| Liquidation            |  ✔            |
| Orderbook L2           |  ✔            |
| Orderbook L10          |                |
| Public notifications   |                |
| Quote                  |  ✔            |
| Quote bin 1m           |                |
| Quote bin 5m           |                |
| Quote bin 1h           |                |
| Quote bin 1d           |                |
| Settlement             |                |
| Trade                  |  ✔            |
| Trade bin 1m           |  ✔            |
| Trade bin 5m           |  ✔            |
| Trade bin 1h           |  ✔            |
| Trade bin 1d           |  ✔            |

| AUTHENTICATED          |    Covered     |  
|------------------------|:--------------:|
| Affilate               |                |
| Execution              |                |
| Order                  |  ✔            |
| Margin                 |                |
| Position               |  ✔            |
| Private notifications  |                |
| Transact               |                |
| Wallet                 |  ✔            |

**Pull Requests are welcome!**

### Multi-threading

Observables from Reactive Extensions are single threaded by default. It means that your code inside subscriptions is called synchronously and as soon as the message comes from websocket API. It brings a great advantage of not to worry about synchronization, but if your code takes a longer time to execute it will block the receiving method, buffer the messages and may end up losing messages. For that reason consider to handle messages on the other thread and unblock receiving thread as soon as possible. I've prepared a few examples for you: 

#### Default behavior

Every subscription code is called on a main websocket thread. Every subscription is synchronized together. No parallel execution. It will block the receiving thread. 

```csharp
client
    .Streams
    .TradesStream
    .Subscribe(trade => { code1 });

client
    .Streams
    .BookStream
    .Subscribe(book => { code2 });

// 'code1' and 'code2' are called in a correct order, according to websocket flow
// ----- code1 ----- code1 ----- ----- code1
// ----- ----- code2 ----- code2 code2 -----
```

#### Parallel subscriptions 

Every single subscription code is called on a separate thread. Every single subscription is synchronized, but different subscriptions are called in parallel. 

```csharp
client
    .Streams
    .TradesStream
    .ObserveOn(TaskPoolScheduler.Default)
    .Subscribe(trade => { code1 });

client
    .Streams
    .BookStream
    .ObserveOn(TaskPoolScheduler.Default)
    .Subscribe(book => { code2 });

// 'code1' and 'code2' are called in parallel, do not follow websocket flow
// ----- code1 ----- code1 ----- code1 -----
// ----- code2 code2 ----- code2 code2 code2
```

 #### Parallel subscriptions with synchronization

In case you want to run your subscription code on the separate thread but still want to follow websocket flow through every subscription, use synchronization with gates: 

```csharp
private static readonly object GATE1 = new object();
client
    .Streams
    .TradesStream
    .ObserveOn(TaskPoolScheduler.Default)
    .Synchronize(GATE1)
    .Subscribe(trade => { code1 });

client
    .Streams
    .BookStream
    .ObserveOn(TaskPoolScheduler.Default)
    .Synchronize(GATE1)
    .Subscribe(book => { code2 });

// 'code1' and 'code2' are called concurrently and follow websocket flow
// ----- code1 ----- code1 ----- ----- code1
// ----- ----- code2 ----- code2 code2 ----
```


### Available for help
I do consulting, please don't hesitate to contact me if you have a custom solution you would like me to implement ([web](http://mkotas.cz/), 
<m@mkotas.cz>)

Donations gratefully accepted.
* [![Donate with Bitcoin](https://en.cryptobadges.io/badge/small/1HfxKZhvm68qK3gE8bJAdDBWkcZ2AFs9pw)](https://en.cryptobadges.io/donate/1HfxKZhvm68qK3gE8bJAdDBWkcZ2AFs9pw)
* [![Donate with Litecoin](https://en.cryptobadges.io/badge/small/LftdENE8DTbLpV6RZLKLdzYzVU82E6dz4W)](https://en.cryptobadges.io/donate/LftdENE8DTbLpV6RZLKLdzYzVU82E6dz4W)
* [![Donate with Ethereum](https://en.cryptobadges.io/badge/small/0xb9637c56b307f24372cdcebd208c0679d4e48a47)](https://en.cryptobadges.io/donate/0xb9637c56b307f24372cdcebd208c0679d4e48a47)
