using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text.Json.Serialization;

namespace WeatherFetch.Api.Data
{
	public class Forecast
	{
		[JsonPropertyName("forecastday")]
		public ImmutableArray<ForecastDay> ForecastDays { get; set; }
	}
}