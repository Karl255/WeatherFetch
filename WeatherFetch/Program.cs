using System;
using System.IO;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
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
			string json = api.GetCurrentWeather("Zagreb");

			// cheap way of formatting the json
			json = JsonSerializer.Serialize(JsonSerializer.Deserialize<JsonElement>(json), new JsonSerializerOptions { WriteIndented = true });

			Console.WriteLine(json);
		}

		private static void BootstrapConfiguration() =>
			Configuration = new ConfigurationBuilder()
				.AddUserSecrets<Program>()
				.Build();
	}
}
