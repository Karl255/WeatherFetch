using WeatherFetch.Api.Data;

namespace WeatherFetch.Api
{
	public interface IWeatherApi
	{
		CurrentWeatherRoot GetCurrentWeather(string location, bool includeAirQuality = false);
		ForecastRoot GetForecast(string location, int days, bool includeAirQuality = false, bool includeAlerts = false);
	}
}