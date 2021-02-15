using Bitmex.Client.Websocket.Client;
using Bitmex.Client.Websocket.Communicator;
using System;
using System.IO;
using System.Text;
using Websocket.Client;

namespace Bitmex.Client.Websocket.Recorder
{
    public class BitmexWebsocketRecorderClient : BitmexWebsocketClient
    {
        private readonly FileStream _recording;
        private readonly string _delimiter;
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

        protected override void HandleMessage(ResponseMessage message)
        {
            var tempString = message + _delimiter;
            IsWriting = true;
            _recording.Write(_uniEncoding.GetBytes(tempString), 0, _uniEncoding.GetByteCount(tempString));
            IsWriting = false;
            base.HandleMessage(message);
        }
    }
}
