using System;

namespace Bitmex.Client.Websocket.Responses.Fundings
{
    public class Funding
    {
        public DateTime Timestamp { get; set; }

        public string Symbol { get; set; }

        public DateTime FundingInterval { get; set; }

        public double FundingRate { get; set; }

        public double FundingRateDaily { get; set; }
    }
}
