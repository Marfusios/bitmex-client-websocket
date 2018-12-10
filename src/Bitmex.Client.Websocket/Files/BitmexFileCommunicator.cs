using System;
using System.IO;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;
using Bitmex.Client.Websocket.Communicator;
using Websocket.Client;

namespace Bitmex.Client.Websocket.Files
{
    public class BitmexFileCommunicator : IBitmexCommunicator
    {
        private readonly Subject<string> _messageReceivedSubject = new Subject<string>();

        public IObservable<string> MessageReceived => _messageReceivedSubject.AsObservable();
        public IObservable<ReconnectionType> ReconnectionHappened => Observable.Empty<ReconnectionType>();
        public IObservable<DisconnectionType> DisconnectionHappened  => Observable.Empty<DisconnectionType>();

        public int ReconnectTimeoutMs { get; set; } = 60 * 1000;
        public int ErrorReconnectTimeoutMs { get; set; } = 60 * 1000;
        public bool IsStarted { get; private set; }
        public bool IsRunning { get; private set; }

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

        public virtual Task Send(string message)
        {
            return Task.CompletedTask;
        }

        public virtual Task SendInstant(string message)
        {
            return Task.CompletedTask;
        }

        public Task Reconnect()
        {
            return Task.CompletedTask;
        }

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
                        _messageReceivedSubject.OnNext(message);
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
