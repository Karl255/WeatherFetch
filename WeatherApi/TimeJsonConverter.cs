using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WeatherFetch.Api
{
	internal class TimeJsonConverter : JsonConverter<DateTime>
	{
		private const string DateTimeFormat = "hh:mm tt";

		public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => reader.TokenType switch
		{
			// if unix epoch time
			JsonTokenType.Number => DateTimeOffset.FromUnixTimeSeconds(reader.GetInt64()).DateTime,
			JsonTokenType.String => DateTime.ParseExact(reader.GetString(), DateTimeFormat, null),
			_ => throw new Exception("Invalid JSON: trying to parse DateTime from unsupported format.")
		};

		public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options) =>
			writer.WriteStringValue(value.ToString(DateTimeFormat));
	}
}
