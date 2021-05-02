using System.Text.Json.Serialization;

namespace WeatherFetch.Api.Data
{
	public class CurrentWeatherRoot
	{
		[JsonPropertyName("location")]
		public Location Location { get; set; }

		[JsonPropertyName("current")]
		public CurrentWeather Current { get; set; }
	}
}
