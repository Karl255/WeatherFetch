using System;
using System.Collections.Specialized;
using System.Text;
using WeatherFetch.Api;

namespace WeatherFetch
{
	public sealed class WeatherFetchCli
	{
		const string IncludeAirQualityOption = "--aqi";
		const string IncludeAlerts = "--alerts";

		private IWeatherApi Api { get; init; }

		public WeatherFetchCli(IWeatherApi weatherApi) => Api = weatherApi;

		public string Run(string[] args)
		{
			if (args.Length < 1)
				return HelpCommand();

			try
			{
				return args[0] switch
				{
					"help" or "-h" or "-help" or "--help" => HelpCommand(),
					"current" => CurrentWeatherCommand(args),
					"forecast" => ForecastCommand(args),
					_ => InvalidCommand(args)
				};
			}
			catch (WeatherApiErrorException ex)
			{
				return $"{ex.Message} ({ex.ErrorCode})";
			}
			catch (Exception ex)
			{
				return $"Error: {ex.Message}";
			}
		}

		private string CurrentWeatherCommand(string[] args)
		{
			var options = ParseOptions(args, 2);
			var current = Api.GetCurrentWeather(args[1], options.ContainsKey(IncludeAirQualityOption));

			return
				  $"[{current.Location.LocalTime:HH:mm}] Current weather for {current.Location.Name}, {current.Location.Region}, {current.Location.Country}\n"
				+  "\n"
				+ $"{current.Current.Condition.Text}\n"
				+ $"Temperature: {current.Current.TemperatureC}°C\n"
				+ $"Precipitation: {current.Current.PrecipitationMm} mm\n"
				+ $"Wind speed: {current.Current.WindSpeedKmh} km/s {current.Current.WindDirection}\n";
		}

		private string ForecastCommand(string[] args)
		{
			var options = ParseOptions(args, 3);

			bool isNumber = int.TryParse(args[2], out int days);
			if (!isNumber || days < 1)
				throw new Exception($"Invalid argument for <days>: {args[2]}");

			var forecast = Api.GetForecast(args[1], days, options.ContainsKey(IncludeAirQualityOption), options.ContainsKey(IncludeAlerts));

			var sb = new StringBuilder();
			sb.Append($"Forecast for {forecast.Location.Name}, {forecast.Location.Region}, {forecast.Location.Country}\n\n");

			sb.Append("Date        Max/min (°C)  Precipitation  Max wind speed\n");
			foreach (var day in forecast.Forecast.ForecastDays)
			{
				sb.Append($"{day.Date:yyyy-MM-dd}  {day.Day.MaxTemperatureC:0.0}/{day.Day.MinTemperatureC:0.0}     {$"{day.Day.TotalPrecipitationMm} mm",-13}  {day.Day.MaxWindSpeedKmh} km/h\n");
			}

			// TODO: alerts

			return sb.ToString();
		}

		private static string HelpCommand()
			=> "Help for WeatherFetch:\n"
			+  "\n"
			+  "Commands:\n"
			+ $"current <location> [{IncludeAirQualityOption}] - Shows the current weather for the specified location.\n"
			+ $"forecast <location> <days> [{IncludeAirQualityOption}] [{IncludeAlerts}] - Shows the hourly forecast for the specified location.\n"
			+  "help - Shows this help.\n"
			+  "\n"
			+  "Options:\n"
			+ $"{IncludeAirQualityOption} - Include air quality data.\n"
			+ $"{IncludeAlerts} - Include weather alerts.\n";

		private static string InvalidCommand(string[] args) => $"Invalid command: {args[0]}\n";

		/// <summary>
		/// Parses CLI options starting with command argumnet at <paramref name="start"/>.
		/// </summary>
		/// <param name="start">The index at which options begin.</param>
		/// <returns></returns>
		private static StringDictionary ParseOptions(string[] args, int start)
		{
			bool lastWasKey = false;
			string lastKey = null;
			StringDictionary options = new();

			for (int i = start; i < args.Length; i++)
			{
				if (args[i].StartsWith('-')) // current is key
				{
					options.Add(lastKey = args[i], null);
					lastWasKey = true;
				}
				else // current is value
				{
					if (lastWasKey)
					{
						options[lastKey] = args[i];
					}
					// value after a value is ignored
					lastWasKey = false;
				}
			}

			return options;
		}
	}
}
