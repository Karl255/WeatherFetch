using System.Text.Json.Serialization;

namespace WeatherFetch.Api.Data
{
	public class ForecastRoot
	{
		[JsonPropertyName("location")]
		public Location Location { get; set; }

		[JsonPropertyName("current")]
		public CurrentWeather Current { get; set; }

		[JsonPropertyName("forecast")]
		public Forecast Forecast { get; set; }
	}
}
