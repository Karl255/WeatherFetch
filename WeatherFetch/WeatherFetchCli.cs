using System;
using System.Collections;
using System.Collections.Specialized;
using WeatherFetch.Api;

namespace WeatherFetch
{
	public class WeatherFetchCli
	{
		private IWeatherApi Api;
		private string[] Args;
		private (int Left, int Right) InitialCursorPosition;

		public WeatherFetchCli(string[] args, IWeatherApi weatherApi)
		{
			Api = weatherApi;
			Args = args;
		}

		public void Run()
		{
			InitialCursorPosition = Console.GetCursorPosition();

			if (Args.Length < 1)
			{
				HelpCommand();
				return;
			}

			_ = Args[0] switch
			{
				"current" => CurrentWeatherCommand(),
				"forecast" => ForecastCommand(),
				"help" or "-help" or "--help" => HelpCommand(),
				_ => InvalidCommand()
			};
		}

		private int CurrentWeatherCommand()
		{
			var options = ParseOptions(2);
			bool includeAirQuality = options.ContainsKey("-aqi");

			var current = Api.GetCurrentWeather(Args[1], includeAirQuality);

			Console.WriteLine($"[{current.Location.LocalTime.ToString("HH:mm")}] Current weather for {current.Location.Name}, {current.Location.Region}, {current.Location.Country}");
			Console.WriteLine();

			Console.WriteLine($"Temperature: {current.Current.TemperatureC}°C");
			Console.WriteLine($"Precipitation: {current.Current.PrecipitationMm} mm");
			Console.WriteLine($"Wind speed: {current.Current.WindSpeedKmh} km/s {current.Current.WindDirection}");

			return 0;
		}

		private int ForecastCommand()
		{
			var options = ParseOptions(3);


			return 0;
		}

		private int HelpCommand()
		{
			string helpContent = "Help for WeatherFetch:\n"
				+ "\n"
				+ "Commands:\n"
				+ "current <location> [-aqi] - Shows the current weather for the specified location.\n"
				+ "forecast <location> <days> [-aqi] [-alerts] - Shows the hourly forecast for the specified location.\n"
				+ "help - Shows this help.\n"
				+ "\n"
				+ "Options:\n"
				+ "-aqi - Include air quality data.\n"
				+ "-alerts - Include weather alerts.\n";

			Console.WriteLine(helpContent);
			return 0;
		}

		private int InvalidCommand()
		{
			Console.WriteLine($"Invalid command: {Args[0]}");
			return 0;
		}

		private StringDictionary ParseOptions(int start)
		{
			bool lastWasKey = false;
			string lastKey = null;
			StringDictionary options = new();

			for (int i = start; i < Args.Length; i++)
			{
				if (Args[i].StartsWith('-')) // current is key
				{
					options.Add(lastKey = Args[i], null);
					lastWasKey = true;
				}
				else // current is value
				{
					if (lastWasKey)
					{
						options[lastKey] = Args[i];
					}
					// value after a value is ignored
					lastWasKey = false;
				}
			}

			return options;
		}
	}
}
