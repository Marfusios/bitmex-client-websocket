using System;

namespace Bitmex.Client.Websocket.Utils
{
    [AttributeUsage(AttributeTargets.Field)]
    public class StringValue : Attribute
    {
        public string Value { get; protected set; }

        public StringValue(string value)
        {
            Value = value;
        }
    }
}