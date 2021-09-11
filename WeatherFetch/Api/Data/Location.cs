using System;
using System.Text.Json.Serialization;

namespace WeatherFetch.Api.Data
{
	public class Location
	{
		[JsonPropertyName("name")]
		public string Name { get; set; }

		[JsonPropertyName("region")]
		public string Region { get; set; }

		[JsonPropertyName("country")]
		public string Country { get; set; }

		[JsonPropertyName("lat")]
		public double Latitude { get; set; }

		[JsonPropertyName("lon")]
		public double Longitude { get; set; }

		[JsonPropertyName("tz_id")]
		public string TimezoneId { get; set; }

		[JsonPropertyName("localtime_epoch")]
		[JsonConverter(typeof(DateTimeJsonConverter))]
		public DateTime LocalTimeUtc { get; set; }

		[JsonPropertyName("localtime")]
		[JsonConverter(typeof(DateTimeJsonConverter))]
		public DateTime LocalTime { get; set; }
	}
}
