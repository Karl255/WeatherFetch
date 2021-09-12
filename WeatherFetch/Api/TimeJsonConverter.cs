using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WeatherFetch.Api
{
	internal class TimeJsonConverter : JsonConverter<DateTime?>
	{
		private const string DateTimeFormat = "hh:mm tt";

		public override DateTime? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			try
			{
				return reader.TokenType switch
				{
					// if unix epoch time
					JsonTokenType.Number => DateTimeOffset.FromUnixTimeSeconds(reader.GetInt64()).DateTime,
					JsonTokenType.String => DateTime.ParseExact(reader.GetString(), DateTimeFormat, null),
					_ => throw new Exception("Invalid JSON: trying to parse DateTime from unsupported format.")
				};
			}
			catch
			{
				// because sometimes the API likes to put invalid time values into some time fields
				return null;
			}
		}

		public override void Write(Utf8JsonWriter writer, DateTime? value, JsonSerializerOptions options) =>
			writer.WriteStringValue(value is null ? "" : value.Value.ToString(DateTimeFormat));
	}
}
