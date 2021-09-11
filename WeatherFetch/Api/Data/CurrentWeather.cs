using System;
using System.Text.Json.Serialization;

#nullable enable

namespace WeatherFetch.Api.Data
{
	public class CurrentWeather
	{
		[JsonPropertyName("last_updated_epoch")]
		[JsonConverter(typeof(DateTimeJsonConverter))]
		public DateTime? LastUpdatedUtc { get; set; }

		[JsonPropertyName("temp_c")]
		public double? TemperatureC { get; set; }

		[JsonPropertyName("temp_f")]
		public double? TemperatureF { get; set; }

		[JsonPropertyName("is_day")]
		public bool? IsDay { get; set; }

		[JsonPropertyName("condition")]
		public Condition? Condition { get; set; }

		[JsonPropertyName("wind_mph")]
		public double? WindSpeedMph { get; set; }

		[JsonPropertyName("wind_kph")]
		public double? WindSpeedKmh { get; set; }

		[JsonPropertyName("wind_degree")]
		public int? WindDegree { get; set; }

		[JsonPropertyName("wind_dir")]
		public string? WindDirection { get; set; }

		[JsonPropertyName("pressure_mb")]
		public double? PressureMBar { get; set; }

		[JsonPropertyName("pressure_in")]
		public double? PressureIn { get; set; }

		[JsonPropertyName("precip_mm")]
		public double? PrecipitationMm { get; set; }

		[JsonPropertyName("precip_in")]
		public double? PrecipitationIn { get; set; }

		[JsonPropertyName("humidity")]
		public int? Humidity { get; set; }

		[JsonPropertyName("cloud")]
		public int? CloudCoverage { get; set; }

		[JsonPropertyName("feelslike_c")]
		public double? FeelsLikeC { get; set; }

		[JsonPropertyName("feelslike_f")]
		public double? FeelsLikesF { get; set; }

		[JsonPropertyName("vis_km")]
		public double? VisibilityKm { get; set; }

		[JsonPropertyName("vis_miles")]
		public double? VisibilityMiles { get; set; }

		[JsonPropertyName("uv")]
		public double? UVIndex { get; set; }

		[JsonPropertyName("gust_mph")]
		public double? WindGustMph { get; set; }

		[JsonPropertyName("gust_kph")]
		public double? WindGustKmh { get; set; }

		[JsonPropertyName("air_quality")]
		public AirQuality? AirQuality { get; set; }
	}
}
