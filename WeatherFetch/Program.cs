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

			var cli = new WeatherFetchCli(
				new WeatherApi(Configuration[ApiKeySecretName])
			);

			System.Console.WriteLine(cli.Run(args));
		}

		private static void BootstrapConfiguration() =>
			Configuration = new ConfigurationBuilder()
				.AddUserSecrets<Program>()
				.Build();
	}
}
