using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Synapse.RestClient
{
    internal class StringUnixDateTimeConverter : DateTimeConverterBase 
    {
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType != JsonToken.String && reader.TokenType != JsonToken.Null)
                throw new Exception("Wrong Token Type");

            if (reader.Value == null)
                return null;

            long ticks;
            if(!long.TryParse((string)reader.Value, out ticks))
                throw new NotSupportedException(string.Format("Value not timestamp: {0}", reader.Value));

            if (objectType == typeof(DateTime) || objectType == typeof(DateTime?))
                return ticks.FromUnixTime();
            else if (objectType == typeof(DateTimeOffset) || objectType == typeof(DateTimeOffset?))
                return ticks.OffsetFromUnixTime();
            else
                throw new NotSupportedException(string.Format("Unsupported Type: {0}", objectType));
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            long? val = null;
            if (value is DateTime)
                val = ((DateTime)value).ToUnixTime();
            else if (value is DateTimeOffset)
                val = ((DateTimeOffset)value).ToUnixTime();
            else if (value is DateTime? && value != null)
                val = ((DateTime?)value).Value.ToUnixTime();
            else if (value is DateTimeOffset? && value != null)
                val = ((DateTimeOffset?)value).Value.ToUnixTime();
            else if ((value is DateTimeOffset? || value is DateTime?) && value == null)
                val = null;
            else
                throw new NotSupportedException(string.Format("Unsupported Type: {0}", value.GetType()));

            writer.WriteValue(val.HasValue ? val.ToString() : null);
        }
    }

    internal class UnixDateTimeMillisecondsConverter : DateTimeConverterBase
    {
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType != JsonToken.Integer && reader.TokenType != JsonToken.Null)
                throw new Exception("Wrong Token Type");

            if (reader.Value == null)
                return null;

            long ticks = (long)reader.Value;

            if (objectType == typeof(DateTime) || objectType == typeof(DateTime?))
                return ticks.FromUnixTimeMilliseconds();
            else if (objectType == typeof(DateTimeOffset) || objectType == typeof(DateTimeOffset?))
                return ticks.OffsetFromUnixTimeMilliseconds();
            else
                throw new NotSupportedException(string.Format("Unsupported Type: {0}", objectType));

        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            long? val = null;
            if (value is DateTime)
                val = ((DateTime)value).ToUnixTimeMilliseconds();
            else if (value is DateTimeOffset)
                val = ((DateTimeOffset)value).ToUnixTimeMilliseconds();
            else if (value is DateTime? && value != null)
                val = ((DateTime?)value).Value.ToUnixTimeMilliseconds();
            else if (value is DateTimeOffset? && value != null)
                val = ((DateTimeOffset?)value).Value.ToUnixTimeMilliseconds();
            else if ((value is DateTimeOffset? || value is DateTime?) && value == null)
                val = null;
            else
                throw new NotSupportedException(string.Format("Unsupported Type: {0}", value.GetType()));

            writer.WriteValue(val);
        }
    }
}
