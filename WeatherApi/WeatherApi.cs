using System;
using System.IO;
using System.Net;
using System.Text;
using System.Text.Json;
using WeatherFetch.Api.Data;

namespace WeatherFetch.Api
{
	public sealed class WeatherApi : IWeatherApi
	{
		private const string BaseUrl = "https://api.weatherapi.com/v1/";

		private string ApiKey { get; init; }
		private JsonSerializerOptions CustomJsonSerializerOptions = new()
		{
			Converters = { new SmartBoolJsonConverter() }
		};

		public WeatherApi(string apiKey)
		{
			ApiKey = apiKey;
		}

		/// <summary>
		/// Base utility method for making API calls.
		/// </summary>
		/// <param name="apiMethod">The constant string of the API method.</param>
		/// <param name="parameters">A list of key-value pairs to append to the URI.</param>
		/// <returns></returns>
		private string ApiFetch(string apiMethod, params (string, string)[] parameters)
		{
			var sb = new StringBuilder($"{BaseUrl}{apiMethod}?key={ApiKey}");

			foreach ((string key, string value) in parameters)
				sb.Append('&')
					.Append(key)
					.Append('=')
					.Append(System.Uri.EscapeDataString(value));

			HttpWebRequest request = WebRequest.CreateHttp(sb.ToString());

			using var response = request.GetResponse();
			using var stream = response.GetResponseStream();
			using var reader = new StreamReader(stream);

			string json = reader.ReadToEnd();

			if (json.StartsWith(@"{""error"":{"))
				throw new WeatherApiErrorException(json);

			return json;
		}

		public CurrentWeatherRoot GetCurrentWeather(string location, bool includeAirQuality = false)
		{
			string json = ApiFetch(
				ApiMethod.CurrentWeather,
				("q", location),
				("aqi", includeAirQuality ? "yes" : "no")
			);

			return JsonSerializer.Deserialize<CurrentWeatherRoot>(json, CustomJsonSerializerOptions);
		}

		public ForecastRoot GetForecast(string location, int days, bool includeAirQuality = false, bool includeAlerts = false)
		{
			if (includeAlerts)
				throw new NotImplementedException("GetForecast: Alerts are not supported yet.");

			string json = ApiFetch(
				ApiMethod.Forecast,
				("q", location),
				("days", days.ToString()),
				("aqi", includeAirQuality ? "yes" : "no"),
				("alerts", includeAlerts ? "yes" : "no")
			);

			return JsonSerializer.Deserialize<ForecastRoot>(json, CustomJsonSerializerOptions);
		}
	}
}
