using System;

namespace Bitmex.Client.Websocket.Exceptions
{
    public class BitmexBadInputException : BitmexException
    {
        public BitmexBadInputException()
        {
        }

        public BitmexBadInputException(string message) : base(message)
        {
        }

        public BitmexBadInputException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
