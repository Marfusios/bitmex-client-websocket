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
            return "GET/realtime" + nonce;
        }

        public static string CreateSignature(string key, string message)
        {
            var keyBytes = Encoding.UTF8.GetBytes(key);
            var messageBytes = Encoding.UTF8.GetBytes(message);


            using var hmacSha256 = new HMACSHA256(keyBytes);
            byte[] hashMessage = hmacSha256.ComputeHash(messageBytes);
            return ToLowerHex(hashMessage);
        }

        private static string ToLowerHex(byte[] bytes)
        {
            var chars = new char[bytes.Length * 2];
            for (var i = 0; i < bytes.Length; i++)
            {
                var value = bytes[i];
                chars[i * 2] = GetLowerHexChar(value >> 4);
                chars[i * 2 + 1] = GetLowerHexChar(value & 0xF);
            }

            return new string(chars);
        }

        private static char GetLowerHexChar(int value)
        {
            return (char)(value < 10 ? '0' + value : 'a' + value - 10);
        }
    }
}
