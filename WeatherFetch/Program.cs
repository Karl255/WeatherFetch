using System;
using Microsoft.Extensions.Configuration;
using WeatherFetch.Api;

namespace WeatherFetch
{
	class Program
	{
		private static IConfigurationRoot Configuration;
		private const string ApiKeySecretName = "Weather:ApiKey";

		static void Main(string[] args)
		{
			BootstrapConfiguration();

			var api = new WeatherApi(Configuration[ApiKeySecretName]);
			var forecast = api.GetForecast("Zagreb", 3);

			foreach (var forecastDay in forecast.Forecast.ForecastDays)
			{
				string str = $"{forecastDay.Date?.ToString("yyyy-MM-dd") ?? "unknown"}"
					+ $" {forecastDay.Day.MinTemperatureC,4:0.0}°C -"
					+ $" {forecastDay.Day.MaxTemperatureC,4:0.0}°C";

				Console.WriteLine(str);
			}
		}

		private static void BootstrapConfiguration() =>
			Configuration = new ConfigurationBuilder()
				.AddUserSecrets<Program>()
				.Build();
	}
}
