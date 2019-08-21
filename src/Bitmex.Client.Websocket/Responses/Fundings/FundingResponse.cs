using System.Reactive.Subjects;
using Bitmex.Client.Websocket.Json;
using Bitmex.Client.Websocket.Messages;
using Newtonsoft.Json.Linq;

namespace Bitmex.Client.Websocket.Responses.Fundings
{
    /// <summary>
    /// Fundings response
    /// </summary>
    public class FundingResponse : ResponseBase
    {
        /// <summary>
        /// Operation type
        /// </summary>
        public override MessageType Op => MessageType.Funding;

        /// <summary>
        /// All latest fundings
        /// </summary>
        public Funding[] Data { get; set; }

        internal static bool TryHandle(JObject response, ISubject<FundingResponse> subject)
        {
            if (response?["table"]?.Value<string>() != "funding")
                return false;

            var parsed = response.ToObject<FundingResponse>(BitmexJsonSerializer.Serializer);
            subject.OnNext(parsed);

            return true;
        }
    }
}
