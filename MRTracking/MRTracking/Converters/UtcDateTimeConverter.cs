using System.Text.Json;
using System.Text.Json.Serialization;

namespace MRTracking.Converters
{
    public class UtcDateTimeConverter : JsonConverter<DateTime>
    {
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var dateTime = reader.GetDateTime();
            
            // If the DateTime is already UTC, return as is
            if (dateTime.Kind == DateTimeKind.Utc)
            {
                return dateTime;
            }
            
            // If it's Unspecified or Local, convert to UTC
            // Treat Unspecified as UTC to avoid timezone conversion issues
            if (dateTime.Kind == DateTimeKind.Unspecified)
            {
                return DateTime.SpecifyKind(dateTime, DateTimeKind.Utc);
            }
            
            // If it's Local, convert to UTC
            return dateTime.ToUniversalTime();
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            // Always write as UTC
            var utcValue = value.Kind == DateTimeKind.Utc ? value : 
                          value.Kind == DateTimeKind.Unspecified ? DateTime.SpecifyKind(value, DateTimeKind.Utc) : 
                          value.ToUniversalTime();
            
            writer.WriteStringValue(utcValue);
        }
    }

    public class NullableUtcDateTimeConverter : JsonConverter<DateTime?>
    {
        public override DateTime? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Null)
            {
                return null;
            }

            var dateTime = reader.GetDateTime();
            
            // If the DateTime is already UTC, return as is
            if (dateTime.Kind == DateTimeKind.Utc)
            {
                return dateTime;
            }
            
            // If it's Unspecified or Local, convert to UTC
            // Treat Unspecified as UTC to avoid timezone conversion issues
            if (dateTime.Kind == DateTimeKind.Unspecified)
            {
                return DateTime.SpecifyKind(dateTime, DateTimeKind.Utc);
            }
            
            // If it's Local, convert to UTC
            return dateTime.ToUniversalTime();
        }

        public override void Write(Utf8JsonWriter writer, DateTime? value, JsonSerializerOptions options)
        {
            if (value == null)
            {
                writer.WriteNullValue();
                return;
            }

            // Always write as UTC
            var utcValue = value.Value.Kind == DateTimeKind.Utc ? value.Value : 
                          value.Value.Kind == DateTimeKind.Unspecified ? DateTime.SpecifyKind(value.Value, DateTimeKind.Utc) : 
                          value.Value.ToUniversalTime();
            
            writer.WriteStringValue(utcValue);
        }
    }
}

