using System;
using System.IO;
using System.Net.WebSockets;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;
using Bitmex.Client.Websocket.Communicator;
using Websocket.Client;
using Websocket.Client.Models;

namespace Bitmex.Client.Websocket.Files
{
    /// <summary>
    /// Communicator that loads raw backtest data from file and streams
    /// </summary>
    public class BitmexFileCommunicator : IBitmexCommunicator
    {
        private readonly Subject<ResponseMessage> _messageReceivedSubject = new Subject<ResponseMessage>();

        public IObservable<ResponseMessage> MessageReceived => _messageReceivedSubject.AsObservable();
        public IObservable<ReconnectionInfo> ReconnectionHappened => Observable.Empty<ReconnectionInfo>();
        public IObservable<DisconnectionInfo> DisconnectionHappened  => Observable.Empty<DisconnectionInfo>();

        public TimeSpan? ReconnectTimeout { get; set; } = TimeSpan.FromSeconds(60);
        public TimeSpan? ErrorReconnectTimeout { get; set; } = TimeSpan.FromSeconds(60);
        public string Name { get; set; }
        public bool IsStarted { get; private set; }
        public bool IsRunning { get; private set; }
        public bool IsReconnectionEnabled { get; set; }
        public bool IsTextMessageConversionEnabled { get; set; }
        public ClientWebSocket NativeClient { get; }
        public Encoding MessageEncoding { get; set; }

        public string[] FileNames { get; set; }
        public string Delimiter { get; set; }
        public Encoding Encoding { get; set; } = Encoding.UTF8;

        public virtual void Dispose()
        {
            
        }

        public virtual Task Start()
        {
            StartStreaming();

            return Task.CompletedTask;
        }

        public Task StartOrFail()
        {
            return Task.CompletedTask;
        }

        public Task<bool> Stop(WebSocketCloseStatus status, string statusDescription)
        {
            return Task.FromResult(true);
        }

        public Task<bool> StopOrFail(WebSocketCloseStatus status, string statusDescription)
        {
            return Task.FromResult(true);
        }

        public virtual void Send(string message)
        {
        }

        public void Send(byte[] message)
        {
        }

        public virtual Task SendInstant(string message)
        {
            return Task.CompletedTask;
        }

        public Task SendInstant(byte[] message)
        {
            return Task.CompletedTask;
        }

        public Task Reconnect()
        {
            return Task.CompletedTask;
        }

        public Task ReconnectOrFail()
        {
            throw new NotImplementedException();
        }

        public void StreamFakeMessage(ResponseMessage message)
        {
            throw new NotImplementedException();
        }

        public Uri Url { get; set; }

        private void StartStreaming()
        {
            if (FileNames == null)
                throw new InvalidOperationException("FileNames are not set, provide at least one path to historical data");
            if(string.IsNullOrEmpty(Delimiter))
                throw new InvalidOperationException("Delimiter is not set (separator between messages in the file)");

            foreach (var fileName in FileNames)
            {
                var fs = new FileStream(fileName, FileMode.Open);
                var stream = new StreamReader(fs, Encoding);
                using (stream)
                {
                    var message = ReadByDelimeter(stream, Delimiter);
                    while (message != null)
                    {
                        _messageReceivedSubject.OnNext(ResponseMessage.TextMessage(message));
                        message = ReadByDelimeter(stream, Delimiter);
                    }
                }
            }
        }

 
        private static string ReadByDelimeter(StreamReader sr, string delimiter)
        {
            var line = new StringBuilder();
            int matchIndex = 0;

            while (sr.Peek() > 0)
            {               
                var nextChar = (char)sr.Read();
                line.Append(nextChar);
                if (nextChar == delimiter[matchIndex])
                {
                    if(matchIndex == delimiter.Length - 1)
                    {
                        return line.ToString().Substring(0, line.Length - delimiter.Length);
                    }
                    matchIndex++;
                }
                else
                {
                    matchIndex = 0;
                }
            }

            return line.Length == 0 ? null : line.ToString();
        }
    }
}
