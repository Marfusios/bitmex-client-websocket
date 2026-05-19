using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;

namespace Bitmex.Client.Websocket.Utils
{
    public static class EnumUtility
    {
        private static readonly ConcurrentDictionary<Type, IReadOnlyDictionary<object, string>> StringValuesByEnumValue = new ConcurrentDictionary<Type, IReadOnlyDictionary<object, string>>();
        private static readonly ConcurrentDictionary<Type, IReadOnlyDictionary<string, object>> EnumValuesByStringValue = new ConcurrentDictionary<Type, IReadOnlyDictionary<string, object>>();

        public static string GetStringValue(this Enum e)
        {
            var values = StringValuesByEnumValue.GetOrAdd(e.GetType(), CreateStringValuesByEnumValue);
            return values[e];
        }

        public static T GetAttribute<T>(this Enum e) where T : Attribute
        {
            var name = Enum.GetName(e.GetType(), e);
            if (name == null)
                throw new InvalidOperationException($"Unable to find enum field for value '{e}'.");

            var field = e.GetType().GetField(name);
            var attribute = field?.GetCustomAttribute<T>();
            if (attribute == null)
                throw new InvalidOperationException($"Unable to find attribute '{typeof(T).Name}' on enum field '{name}'.");

            return attribute;
        }

        public static T GetFieldByStringValue<T>(this T t, string expected)
        {
            var fields = EnumValuesByStringValue.GetOrAdd(typeof(T), CreateEnumValuesByStringValue);

            if (fields.TryGetValue(expected, out var value))
                return (T)value;

            return default(T);
        }

        private static IReadOnlyDictionary<object, string> CreateStringValuesByEnumValue(Type type)
        {
            var values = new Dictionary<object, string>();

            foreach (var fieldInfo in type.GetFields(BindingFlags.Public | BindingFlags.Static))
            {
                var stringValueAttribute = fieldInfo.GetCustomAttribute<StringValue>();
                var fieldValue = fieldInfo.GetValue(null);
                if (stringValueAttribute != null && fieldValue != null)
                    values[fieldValue] = stringValueAttribute.Value;
            }

            return values;
        }

        private static IReadOnlyDictionary<string, object> CreateEnumValuesByStringValue(Type type)
        {
            var values = new Dictionary<string, object>();

            foreach (var fieldInfo in type.GetFields(BindingFlags.Public | BindingFlags.Static))
            {
                var stringValueAttribute = fieldInfo.GetCustomAttribute<StringValue>();
                var fieldValue = fieldInfo.GetValue(null);
                if (stringValueAttribute != null && fieldValue != null)
                    values[stringValueAttribute.Value] = fieldValue;
            }

            return values;
        }
    }
}
