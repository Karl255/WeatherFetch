using System;
using System.Text.Json.Serialization;

#nullable enable

namespace WeatherFetch.Api.Data
{
	public class Hour
	{
		[JsonPropertyName("time_epoch")]
		[JsonConverter(typeof(DateTimeJsonConverter))]
		public DateTime? TimeEpoch { get; set; }
		
		[JsonPropertyName("time")]
		[JsonConverter(typeof(DateTimeJsonConverter))]
		public DateTime? Time { get; set; }
		
		[JsonPropertyName("temp_c")]
		public double? TemperatureC { get; set; }
		
		[JsonPropertyName("temp_f")]
		public double? TemperatureF { get; set; }
		
		[JsonPropertyName("is_day")]
		public bool? IsDay { get; set; }
		
		[JsonPropertyName("condition")]
		public Condition? Condition { get; set; }
		
		[JsonPropertyName("wind_mph")]
		public double? WindMph { get; set; }
		
		[JsonPropertyName("wind_kph")]
		public double? WindKmh { get; set; }
		
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
		public int? CloudCover { get; set; }
		
		[JsonPropertyName("feelslike_c")]
		public double? FeelsLikeC { get; set; }
		
		[JsonPropertyName("feelslike_f")]
		public double? FeelsLikeF { get; set; }
		
		[JsonPropertyName("windchill_c")]
		public double? WindChillC { get; set; }
		
		[JsonPropertyName("windchill_f")]
		public double? WindChillF { get; set; }
		
		[JsonPropertyName("heatindex_c")]
		public double? HeatIndexC { get; set; }
		
		[JsonPropertyName("heatindex_f")]
		public double? HeatIndexF { get; set; }
		
		[JsonPropertyName("dewpoint_c")]
		public double? DewPointC { get; set; }
		
		[JsonPropertyName("dewpoint_f")]
		public double? DewPointF { get; set; }
		
		[JsonPropertyName("will_it_rain")]
		public int? WillItRain { get; set; }
		
		[JsonPropertyName("chance_of_rain")]
		public int? ChanceOfRain { get; set; }
		
		[JsonPropertyName("will_it_snow")]
		public int? WillItSnow { get; set; }
		
		[JsonPropertyName("chance_of_snow")]
		public int? ChanceOfSnow { get; set; }
		
		[JsonPropertyName("vis_km")]
		public double? VisibilityKm { get; set; }
		
		[JsonPropertyName("vis_miles")]
		public double? VisibilityMiles { get; set; }
		
		[JsonPropertyName("gust_mph")]
		public double? WindGustMph { get; set; }
		
		[JsonPropertyName("gust_kph")]
		public double? WindGustKmh { get; set; }
		
		[JsonPropertyName("uv")]
		public double? UVIndex { get; set; }
	}
}