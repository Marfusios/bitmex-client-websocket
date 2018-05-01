using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Serilog;

namespace Bitmex.Client.Websocket.Responses.Wallets
{
    class WalletConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Wallet);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer)
        {
            var array = JArray.Load(reader);
            return JArrayToTradingTicker(array);
        }

        public override bool CanWrite => false;

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        private Wallet JArrayToTradingTicker(JArray array)
        {
            return new Wallet
            {
                Type = ParseWalletType((string)array[0]),
                Currency = (string)array[1],
                Balance = (double)array[2],
                UnsettledInterest = (double)array[3],
                BalanceAvailable = (double?)array[4]
            };
        }

        public static WalletType ParseWalletType(string type)
        {
            if (string.IsNullOrWhiteSpace(type))
                return WalletType.Undefined;
            var safe = type.ToLower().Trim();
            switch (safe)
            {
                case "exchange":
                case var s when s.StartsWith("exchange"):
                    return WalletType.Exchange;
                case "margin":
                case var s when s.StartsWith("margin"):
                    return WalletType.Margin;
                case "funding":
                case var s when s.StartsWith("funding"):
                    return WalletType.Funding;
            }
            Log.Warning("Can't parse WalletType, input: " + safe);
            return WalletType.Undefined;
        }
    }
}
