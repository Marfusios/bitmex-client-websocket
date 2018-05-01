using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Bitmex.Client.Websocket.Responses.Tickers
{
    class TickerConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Ticker);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer)
        {
            var array = JArray.Load(reader);


            //if (symbol.StartsWith("t")) return JArrayToTradingTicker(array);
            //if (symbol.StartsWith("f")) return JArrayToFundingTicker(array);
            //return null;

            return JArrayToTradingTicker(array);
        }

        public override bool CanWrite => false;

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        private Ticker JArrayToTradingTicker(JArray array)
        {
            return new Ticker
            {
                Bid = (double)array[0],
                BidSize = (double)array[1],
                Ask = (double)array[2],
                AskSize = (double)array[3],
                DailyChange = (double)array[4],
                DailyChangePercent = (double)array[5],
                LastPrice = (double)array[6],
                Volume = (double)array[7],
                High = (double)array[8],
                Low = (double)array[9]
            };
        }

        //private FundingTicker JArrayToFundingTicker(JArray array)
        //{
        //    string symbol = (string)array[0];
        //    string symbolStripped = symbol.Substring(1);

        //    return new FundingTicker
        //    {
        //        Symbol = symbolStripped,
        //        FlashReturnRate = (double)array[1],
        //        Bid = (double)array[2],
        //        BidPeriod = (int)array[3],
        //        BidSize = (double)array[4],
        //        Ask = (double)array[5],
        //        AskPeriod = (int)array[6],
        //        AskSize = (double)array[7],
        //        DailyChange = (double)array[8],
        //        DailyChangePercent = (double)array[9],
        //        LastPrice = (double)array[10],
        //        Volume = (double)array[11],
        //        High = (double)array[12],
        //        Low = (double)array[13]
        //    };
        //}
    }
}
