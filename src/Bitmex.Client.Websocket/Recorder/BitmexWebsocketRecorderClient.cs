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

        private readonly FileStream _recording;
        private readonly string _delimiter;
        private bool _stopped = false;
        private readonly UnicodeEncoding _uniEncoding = new UnicodeEncoding();

        /// <summary>
        /// Whether the recorder is currently writing to disk. Used to avoid interupting int he middle of a record.
        /// </summary>
        public bool IsWriting { get; private set; } = false;

        /// <summary>
        /// A BitmexWebsocketClient which records the raw data comming off the websocket
        /// </summary>
        /// <param name="communicator"></param>
        /// <param name="recordingPath">Recording file pathname. Overwritten if it exists.</param>
        /// <param name="delimiter">Separator inserted between records.</param>
        public BitmexWebsocketRecorderClient(IBitmexCommunicator communicator, string recordingPath, string delimiter = ";;")
            : base(communicator)
        {
            _recording = File.Create(recordingPath);
            _delimiter = delimiter;
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
        /// Stop the recording after it finished writing the current record.
        /// </summary>
        public void Stop()
        {
            lock (_locker)
            {
                _stopped = true;
            }
        }

        protected override void HandleMessage(ResponseMessage message)
        {
            var tempString = message + _delimiter;
            lock (_locker)
            {
                if (!_stopped)
                    _recording.Write(_uniEncoding.GetBytes(tempString), 0, _uniEncoding.GetByteCount(tempString));
            }
            base.HandleMessage(message);
        }
    }
}
