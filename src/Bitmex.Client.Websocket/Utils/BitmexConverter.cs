using System;
using Bitmex.Client.Websocket.Exceptions;

namespace Bitmex.Client.Websocket.Utils
{
    public static class BitmexConverter
    {
        public static double ConvertToBtc(string from, double value)
        {
            var safe = (from ?? string.Empty).Trim();


            if (safe == "XBt")
            {
                return ConvertFromSatoshiToBtc(value);
            }

            if (string.IsNullOrWhiteSpace(safe) || safe.ToLower() == "btc" || safe.ToLower() == "xbt")
                return value;

            throw new BitmexException($"Can't convert from '{safe}' to BTC");
        }

        public static double ConvertToBtc(string from, long value)
        {
            var valueDouble = Convert.ToDouble(value);
            return ConvertToBtc(from, valueDouble);
        }

        public static double ConvertFromSatoshiToBtc(long satoshi)
        {
            return satoshi / 100000000.0;
        }

        public static double ConvertFromSatoshiToBtc(double satoshi)
        {
            return satoshi / 100000000.0;
        }
    }
}
