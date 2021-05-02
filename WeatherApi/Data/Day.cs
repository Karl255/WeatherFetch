using System.Text.Json.Serialization;

#nullable enable

namespace WeatherFetch.Api.Data
{
	public class Day
	{
		[JsonPropertyName("maxtemp_c")]
		public double? MaxTemperatureC { get; set; }
		
		[JsonPropertyName("maxtemp_f")]
		public double? MaxTemperatureF { get; set; }
		
		[JsonPropertyName("mintemp_c")]
		public double? MinTemperatureC { get; set; }
		
		[JsonPropertyName("mintemp_f")]
		public double? MinTemperatureF { get; set; }
		
		[JsonPropertyName("avgtemp_c")]
		public double? AveragegTemperatureC { get; set; }
		
		[JsonPropertyName("avgtemp_f")]
		public double? AverageTemperatureF { get; set; }
		
		[JsonPropertyName("maxwind_mph")]
		public double? MaxWindSpeedMph { get; set; }
		
		[JsonPropertyName("maxwind_kph")]
		public double? MaxWindSpeedKmh { get; set; }
		
		[JsonPropertyName("totalprecip_mm")]
		public double? TotalPrecipitationMm { get; set; }
		
		[JsonPropertyName("totalprecip_in")]
		public double? TotalPrecipitationIn { get; set; }
		
		[JsonPropertyName("avgvis_km")]
		public double? AverageVisibilityKm { get; set; }
		
		[JsonPropertyName("avgvis_miles")]
		public double? AverageVisibilityMiles { get; set; }
		
		[JsonPropertyName("avghumidity")]
		public double? AverageHumidity { get; set; }
		
		[JsonPropertyName("daily_will_it_rain")]
		public bool? WillItRain { get; set; }
		
		[JsonPropertyName("daily_chance_of_rain")]
		public string? ChanceOfRain { get; set; }
		
		[JsonPropertyName("daily_will_it_snow")]
		public bool? WillItSnow { get; set; }
		
		[JsonPropertyName("daily_chance_of_snow")]
		public string? ChanceOfSnow { get; set; }
		
		[JsonPropertyName("condition")]
		public Condition? Condition { get; set; }
		
		[JsonPropertyName("uv")]
		public double? UVIndex { get; set; }
	}
}