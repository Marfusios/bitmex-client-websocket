using System.Security.Cryptography;
using System.Text;

namespace Bitmex.Client.Websocket.Utils
{
    public static class BitmexAuthentication
    {

        public static long CreateAuthNonce(long? time = null)
        {
            var timeSafe = time ?? BitmexTime.NowMs();
            return timeSafe * 1000;
        }

        public static string CreateAuthPayload(long nonce)
        {
            return "AUTH" + nonce;
        }

        public static string CreateSignature(string payload, string apiSecret)
        {
            var keyBytes = Encoding.UTF8.GetBytes(payload);
            var secretBytes = Encoding.UTF8.GetBytes(apiSecret);


            string ByteToString(byte[] buff)
            {
                var builder = new StringBuilder();

                for (var i = 0; i < buff.Length; i++)
                {
                    builder.Append(buff[i].ToString("X2")); // hex format
                }
                return builder.ToString();
            }

            using (var hmacsha256 = new HMACSHA384(secretBytes))
            {
                byte[] hashmessage = hmacsha256.ComputeHash(keyBytes);
                return ByteToString(hashmessage).ToLower();
            }
        }
    }
}
