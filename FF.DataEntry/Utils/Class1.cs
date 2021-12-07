using System.Text.Json;
using System.Text.Json.Serialization;

namespace FF.DataEntry.Utils
{
    public class TimeSpanJsonConverter : JsonConverter<TimeSpan>
    {
        public override TimeSpan Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            //DateTimeOffset.ParseExact(reader.GetString(),
            //    "MM/dd/yyyy", CultureInfo.InvariantCulture);
            return TimeSpan.Zero;
        }

        public override void Write(Utf8JsonWriter writer, TimeSpan dateTimeValue, JsonSerializerOptions options)
        {
            //        writer.WriteStringValue(dateTimeValue.ToString(
            //            "MM/dd/yyyy", CultureInfo.InvariantCulture));

        }
    }
}
