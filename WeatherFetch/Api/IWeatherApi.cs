using WeatherFetch.Api.Data;

namespace WeatherFetch.Api
{
	public interface IWeatherApi
	{
		public CurrentWeatherRoot GetCurrentWeather(string location, bool includeAirQuality = false);
		public ForecastRoot GetForecast(string location, int days, bool includeAirQuality = false, bool includeAlerts = false);
		public ForecastRoot GetHistory(string location, string date, string endDate = null, bool includeAirQuality = false, bool includeAlerts = false);
	}
}
