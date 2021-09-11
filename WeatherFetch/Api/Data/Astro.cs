using System;
using System.Text.Json.Serialization;

#nullable enable

namespace WeatherFetch.Api.Data
{
	public class Astro
	{
		[JsonPropertyName("sunrise")]
		[JsonConverter(typeof(TimeJsonConverter))]
		public DateTime? Sunrise { get; set; }
		
		[JsonPropertyName("sunset")]
		[JsonConverter(typeof(TimeJsonConverter))]
		public DateTime? Sunset { get; set; }
		
		[JsonPropertyName("moonrise")]
		[JsonConverter(typeof(TimeJsonConverter))]
		public DateTime? Moonrise { get; set; }
		
		[JsonPropertyName("moonset")]
		[JsonConverter(typeof(TimeJsonConverter))]
		public DateTime? Moonset { get; set; }
		
		[JsonPropertyName("moon_phase")]
		public string? MoonPhase { get; set; }
		
		[JsonPropertyName("moon_illumination")]
		public string? MoonIllumination { get; set; }
	}
}