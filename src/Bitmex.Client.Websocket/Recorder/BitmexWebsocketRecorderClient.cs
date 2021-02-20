using Bitmex.Client.Websocket.Client;
using Bitmex.Client.Websocket.Communicator;
using Bitmex.Client.Websocket.Logging;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Websocket.Client;
using Utf8Json;

namespace Bitmex.Client.Websocket.Recorder
{
    public class BitmexWebsocketRecorderClient : BitmexWebsocketClient, IDisposable
    {
        private static readonly ILog Log = LogProvider.GetCurrentClassLogger();
        private static readonly object _locker = new object();

        private readonly FileStream _recordingStream;
        private readonly TextWriter _recorder;

        private readonly string _delimiter;
        private bool _stopped = false;
        private readonly UnicodeEncoding _uniEncoding = new UnicodeEncoding();

        /// <summary>
        /// A BitmexWebsocketClient which records the raw data comming off the websocket
        /// </summary>
        /// <param name="communicator"></param>
        /// <param name="recordingPath">Recording file pathname. Overwritten if it exists. If null recording is disabled.</param>
        /// <param name="delimiter">Separator inserted between records.</param>
        public BitmexWebsocketRecorderClient(IBitmexCommunicator communicator, string recordingPath = null, string delimiter = ";;")
            : base(communicator)
        {
            if (recordingPath == null)
            {
                _stopped = true;
            }
            else
            {
                _recordingStream = File.Create(recordingPath);
                _recorder = new StreamWriter(_recordingStream);
                _delimiter = delimiter;
            }
        }

        /// <summary>
        /// This ensures that the current record finished writing before disposal is complete.
        /// </summary>
        public void Dispose()
        {
            Stop();
            Log.Debug("Stopped BitMEX Websocket Recorder.");
        }
 
        /// <summary>
        /// Stop the recording after it finishes writing the current record.
        /// </summary>
        public void Stop()
        {
            // Maybe it is enough to just flush without any locking?
            lock (_locker)
            {
                _stopped = true;
                _recorder.Flush();
            }
        }

        protected override void HandleMessage(ResponseMessage message)
        {
            var tempString = message + _delimiter;
            lock (_locker)
            {
                if (!_stopped)
                    _recorder.Write(tempString);
            }
            base.HandleMessage(message);
        }
    }
}
