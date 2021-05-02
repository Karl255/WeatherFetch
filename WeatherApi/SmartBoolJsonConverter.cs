using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WeatherFetch.Api
{
	internal class SmartBoolJsonConverter : JsonConverter<bool>
	{
		public override bool Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => reader.TokenType switch
		{
			JsonTokenType.True => true,
			JsonTokenType.False => false,
			JsonTokenType.Number => reader.GetInt32() != 0,
			_ => throw new Exception("Invalid JSON: trying to parse boolean from unsupported format.")
		};

		public override void Write(Utf8JsonWriter writer, bool value, JsonSerializerOptions options) =>
			writer.WriteBooleanValue(value);
	}
}
