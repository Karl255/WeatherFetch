using System;
using System.Collections.Immutable;
using System.Text.Json.Serialization;

#nullable enable

namespace WeatherFetch.Api.Data
{
	public class ForecastDay
	{
		[JsonPropertyName("date")]
		[JsonConverter(typeof(DateJsonConverter))]
		public DateTime? Date { get; set; }

		[JsonPropertyName("date_epoch")]
		[JsonConverter(typeof(DateJsonConverter))]
		public DateTime? DateEpoch { get; set; }

		[JsonPropertyName("day")]
		public Day? Day { get; set; }

		[JsonPropertyName("astro")]
		public Astro? Astro { get; set; }

		[JsonPropertyName("hour")]
		public Hour[]? Hours { get; set; }
	}
}