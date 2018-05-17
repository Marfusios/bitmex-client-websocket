using System;
using System.Net.WebSockets;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Bitmex.Client.Websocket.Validations;
using Serilog;

namespace Bitmex.Client.Websocket.Websockets
{
    public class BitmexWebsocketCommunicator : IDisposable
    {
        private readonly Uri _url;
        private Timer _lastChanceTimer;
        private readonly Func<ClientWebSocket> _clientFactory;

        private DateTime _lastReceivedMsg = DateTime.UtcNow; 

        private bool _disposing = false;
        private ClientWebSocket _client;
        private CancellationTokenSource _cancelation;

        private readonly Subject<string> _messageReceivedSubject = new Subject<string>();


        public BitmexWebsocketCommunicator(Uri url, Func<ClientWebSocket> clientFactory = null)
        {
            BmxValidations.ValidateInput(url, nameof(url));

            _url = url;
            _clientFactory = clientFactory ?? (() => new ClientWebSocket()
            {
                Options = {KeepAliveInterval = new TimeSpan(0, 0, 5, 0)}
            }); 
        }

        /// <summary>
        /// Stream with raw received message
        /// </summary>
        public IObservable<string> MessageReceived => _messageReceivedSubject.AsObservable();

        /// <summary>
        /// Time range in ms, how long to wait before reconnecting if no message comes from server.
        /// Default 60000 ms (1 minute)
        /// </summary>
        public int ReconnectTimeoutMs { get; set; } = 60 * 1000;

        /// <summary>
        /// Time range in ms, how long to wait before reconnecting if last reconnection failed.
        /// Default 60000 ms (1 minute)
        /// </summary>
        public int ErrorReconnectTimeoutMs { get; set; } = 60 * 1000;

        public void Dispose()
        {
            _disposing = true;
            Log.Debug(L("Disposing.."));
            _lastChanceTimer?.Dispose();
            _cancelation?.Cancel();
            _client?.Abort();
            _client?.Dispose();
            _cancelation?.Dispose();
        }

        public Task Start()
        {
            Log.Debug(L("Starting.."));
            _cancelation = new CancellationTokenSource();

            return StartClient(_url, _cancelation.Token);
        }

        public async Task Send(string message)
        {
            BmxValidations.ValidateInput(message, nameof(message));

            Log.Verbose(L($"Sending:  {message}"));
            var buffer = Encoding.UTF8.GetBytes(message);
            var messageSegment = new ArraySegment<byte>(buffer);
            var client = await GetClient();
            await client.SendAsync(messageSegment, WebSocketMessageType.Text, true, _cancelation.Token);
        }

        private async Task StartClient(Uri uri, CancellationToken token)
        {
            DeactiveLastChance();
            _client = _clientFactory();
            
            try
            {
                await _client.ConnectAsync(uri, token);
#pragma warning disable 4014
                Listen(_client, token);
#pragma warning restore 4014               
                ActivateLastChance();
            }
            catch (Exception e)
            {
                Log.Error(e, L("Exception while connecting. " +
                               $"Waiting {ErrorReconnectTimeoutMs/1000} sec before next reconnection try."));
                await Task.Delay(ErrorReconnectTimeoutMs, token);
                await Reconnect();
            }
            
        }

        private async Task<ClientWebSocket> GetClient()
        {
            if (_client == null || (_client.State != WebSocketState.Open && _client.State != WebSocketState.Connecting))
            {
                await Reconnect();
            }
            return _client;
        }

        private async Task Reconnect()
        {
            if (_disposing)
                return;
            Log.Debug(L("Reconnecting..."));
            _cancelation.Cancel();
            await Task.Delay(1000);

            _cancelation = new CancellationTokenSource();
            await StartClient(_url, _cancelation.Token);
        }

        private async Task Listen(ClientWebSocket client, CancellationToken token)
        {
            do
            {
                WebSocketReceiveResult result = null;
                var buffer = new byte[1000];
                var message = new ArraySegment<byte>(buffer);
                var resultMessage = new StringBuilder();
                do
                {
                    result = await client.ReceiveAsync(message, token);
                    var receivedMessage = Encoding.UTF8.GetString(buffer, 0, result.Count);
                    resultMessage.Append(receivedMessage);
                    if (result.MessageType != WebSocketMessageType.Text)
                        break;

                } while (!result.EndOfMessage);

                var received = resultMessage.ToString();
                Log.Verbose(L($"Received:  {received}"));
                _lastReceivedMsg = DateTime.UtcNow;
                _messageReceivedSubject.OnNext(received);

            } while (client.State == WebSocketState.Open && !token.IsCancellationRequested);
        }

        private void ActivateLastChance()
        {
            var timerMs = 1000 * 5;
            _lastChanceTimer = new Timer(async x => await LastChance(x), null, timerMs, timerMs);
        }

        private void DeactiveLastChance()
        {
            _lastChanceTimer?.Dispose();
            _lastChanceTimer = null;
        }

        private async Task LastChance(object state)
        {
            var timeoutMs = Math.Abs(ReconnectTimeoutMs);
            var halfTimeoutMs = timeoutMs / 1.5;
            var diffMs = Math.Abs(DateTime.UtcNow.Subtract(_lastReceivedMsg).TotalMilliseconds);
            if(diffMs > halfTimeoutMs)
                Log.Debug(L($"Last message received {diffMs:F} ms ago"));
            if (diffMs > timeoutMs)
            {
                Log.Debug(L($"Last message received more than {timeoutMs:F} ms ago. Hard restart.."));

                _client?.Abort();
                _client?.Dispose();
                await Reconnect();
            }
        }

        private string L(string msg)
        {
            return $"[BMX WEBSOCKET COMMUNICATOR] {msg}";
        }
    }
}
