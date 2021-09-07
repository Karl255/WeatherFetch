using System;
using System.Collections.Specialized;
using WeatherFetch.Api;

namespace WeatherFetch
{
	public sealed class WeatherFetchCli
	{
		private IWeatherApi Api { get; init; }

		public WeatherFetchCli(IWeatherApi weatherApi) => Api = weatherApi;

		public string Run(string[] args)
		{
			if (args.Length < 1)
				return HelpCommand();

			return args[0] switch
			{
				"help" or "-h" or "-help" or "--help" => HelpCommand(),
				"current" => CurrentWeatherCommand(args),
				"forecast" => ForecastCommand(args),
				_ => InvalidCommand(args)
			};
		}

		private string CurrentWeatherCommand(string[] args)
		{
			var options = ParseOptions(args, 2);
			var current = Api.GetCurrentWeather(args[1], options.ContainsKey("-aqi"));

			return $"[{current.Location.LocalTime:HH:mm}] Current weather for {current.Location.Name}, {current.Location.Region}, {current.Location.Country}"
				+ "\n"
				+ $"{current.Current.Condition.Text}\n"
				+ $"Temperature: {current.Current.TemperatureC}°C\n"
				+ $"Precipitation: {current.Current.PrecipitationMm} mm\n"
				+ $"Wind speed: {current.Current.WindSpeedKmh} km/s {current.Current.WindDirection}";
		}

		private string ForecastCommand(string[] args)
		{
			var options = ParseOptions(args, 3);

			//var forecast = Api.GetForecast();

			return "TBI";
		}

		private static string HelpCommand()
			=> "Help for WeatherFetch:\n"
			+ "\n"
			+ "Commands:\n"
			+ "current <location> [-aqi] - Shows the current weather for the specified location.\n"
			+ "forecast <location> <days> [-aqi] [-alerts] - Shows the hourly forecast for the specified location.\n"
			+ "help - Shows this help.\n"
			+ "\n"
			+ "Options:\n"
			+ "-aqi - Include air quality data.\n"
			+ "-alerts - Include weather alerts.\n";

		private static string InvalidCommand(string[] args) => $"Invalid command: {args[0]}";

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
