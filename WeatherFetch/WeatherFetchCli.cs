using System;
using System.Collections.Specialized;
using System.Text;
using System.Text.RegularExpressions;
using WeatherFetch.Api;

namespace WeatherFetch
{
	public sealed class WeatherFetchCli
	{
		const string IncludeAirQualityOption = "--aqi";
		const string IncludeAlertsOption = "--alerts";

		private WeatherApi Api { get; init; }

		public WeatherFetchCli(WeatherApi weatherApi) => Api = weatherApi;

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
					"date" => HistoryCommand(args),
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
			var current = Api.GetCurrentWeather(
				args[1], // location
				options.ContainsKey(IncludeAirQualityOption));

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
				throw new Exception($"Invalid value for <days>: {args[2]}");

			var forecast = Api.GetForecast(
				args[1], // location
				days,
				options.ContainsKey(IncludeAirQualityOption),
				options.ContainsKey(IncludeAlertsOption));

			var sb = new StringBuilder();
			sb.Append($"Forecast for {forecast.Location.Name}, {forecast.Location.Region}, {forecast.Location.Country}\n\n");
			sb.Append("Date        Max/min (°C)  Precipitation  Max wind speed\n");

			foreach (var day in forecast.Forecast.ForecastDays)
				sb.Append($"{day.Date:yyyy-MM-dd}  {day.Day.MaxTemperatureC:0.0}/{day.Day.MinTemperatureC:0.0}     {$"{day.Day.TotalPrecipitationMm} mm",-13}  {day.Day.MaxWindSpeedKmh} km/h\n");

			// TODO: alerts

			return sb.ToString();
		}
		
		private string HistoryCommand(string[] args)
		{
			var options = ParseOptions(args, 3);

			if (!(Regex.IsMatch(args[2], @"\d{4}-\d{2}-\d{2}") && DateTime.TryParse(args[2], out _)))
				throw new Exception($"Invalid value for <date>: {args[2]}");

			bool hasEndDate = options.ContainsKey("-t");
			if (hasEndDate && !(Regex.IsMatch(options["-t"], @"\d{4}-\d{2}-\d{2}") && DateTime.TryParse(options["-t"], out _)))
				throw new Exception($"Invalid value for <end_date>: {args[2]}");

			var history = Api.GetHistory(
				args[1], // location
				args[2], // (start) date
				hasEndDate ? options["-t"] : null, // end date (null if none)
				options.ContainsKey(IncludeAirQualityOption),
				options.ContainsKey(IncludeAlertsOption));

			var sb = new StringBuilder();

			string prefix = hasEndDate ? "" : $"[{args[2]}] "; // [yyyy-MM-dd] prefix if only one day
			sb.Append($"{prefix}Forecast for {history.Location.Name}, {history.Location.Region}, {history.Location.Country}\n\n");

			if (hasEndDate) // range of days
			{
				sb.Append("Date        Max/min (°C)  Precipitation  Max wind speed\n");
				foreach (var day in history.Forecast.ForecastDays)
					sb.Append($"{day.Date:yyyy-MM-dd}  {day.Day.MaxTemperatureC:0.0}/{day.Day.MinTemperatureC:0.0}     {$"{day.Day.TotalPrecipitationMm} mm",-13}  {day.Day.MaxWindSpeedKmh} km/h\n");
			}
			else
			{
				var day = history.Forecast.ForecastDays[0];
				sb.Append(
					  $"{day.Day.Condition.Text}\n"
					+ $"Temperature: {day.Day.MaxTemperatureC}°C/{day.Day.MinTemperatureC}°C\n"
					+ $"Precipitation: {day.Day.TotalPrecipitationMm} mm\n"
					+ $"Max wind speed: {day.Day.MaxWindSpeedKmh} km/s\n");
			}

			// TODO: alerts

			return sb.ToString();
		}

		private static string HelpCommand()
			=> "Help for WeatherFetch:\n"
			+  "\n"
			+  "Commands:\n"
			+ $"current <location> [{IncludeAirQualityOption}] - Shows the current weather for the specified location.\n"
			+ $"forecast <location> <days> [{IncludeAirQualityOption}] [{IncludeAlertsOption}] - Shows the hourly forecast for the specified location.\n"
			+ $"date <location> <date> [-t <end_date>] [{IncludeAirQualityOption}] [{IncludeAlertsOption}] - Shows the weather for the specified day or the inclusive range of days.\n"
			+  "help - Shows this help.\n"
			+  "\n"
			+  "Options:\n"
			+ $"{IncludeAirQualityOption} - Include air quality data.\n"
			+ $"{IncludeAlertsOption} - Include weather alerts.\n";

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
