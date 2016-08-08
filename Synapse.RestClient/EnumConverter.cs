using Newtonsoft.Json;
using Synapse.RestClient.Node;
using Synapse.RestClient.Transaction;
using Synapse.RestClient.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Synapse.RestClient
{
    internal abstract class EnumConverter<T> : JsonConverter
    {
        public EnumConverter()
        {
        }

        protected abstract Dictionary<T, string> Lookup { get; }

        public virtual T ToEnum(string str)
        {
            var reversed = Lookup.ToDictionary(kvp => kvp.Value, kvp => kvp.Key);
            if (reversed.ContainsKey(str))
                return reversed[str];

            throw new InvalidOperationException(String.Format("Unknown {0} {1}", typeof(T),  str));
        }

        public virtual string ToString(T val)
        {
            if (Lookup.ContainsKey(val))
                return Lookup[val];

            return null;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            bool isNullable = (objectType.IsGenericType && objectType.GetGenericTypeDefinition() == typeof(Nullable<>));
            Type t = isNullable ? Nullable.GetUnderlyingType(objectType) : objectType;

            if (reader.TokenType == JsonToken.Null)
            {
                if (!isNullable)
                    throw new JsonSerializationException(string.Format("Cannot convert null value to {0}.", objectType));

                return null;
            }

            if (reader.TokenType == JsonToken.String)
            {
                string enumText = reader.Value.ToString();
                return ToEnum(enumText);
            }

            throw new NotImplementedException();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }

            writer.WriteValue(ToString((T)value));
        }

        public override bool CanConvert(Type objectType)
        {
            bool isNullable = (objectType.IsGenericType && objectType.GetGenericTypeDefinition() == typeof(Nullable<>));
            Type t = isNullable ? Nullable.GetUnderlyingType(objectType) : objectType;
            return t == typeof(T);
        }
    }
}
