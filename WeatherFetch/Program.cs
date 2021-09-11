using System;
using WeatherFetch.Api;

namespace WeatherFetch
{
	class Program
	{
		static void Main(string[] args)
		{
			var config = Config.LoadConfig(Config.DefaultLocation);

			WeatherFetchCli cli;

			try
			{
				cli = new WeatherFetchCli(config);
				Console.WriteLine(cli.Run(args));
			}
			catch (UserException ex)
			{
				Console.WriteLine($"Error: {ex.Message}");
			}
			catch (WeatherApiErrorException ex)
			{
				Console.WriteLine($"API error: {ex.Message} ({ex.ErrorCode})");
			}

			if (config.NeedsUpgrade)
				config.StoreConfig(Config.DefaultLocation);
		}
	}
}
