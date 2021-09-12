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
		private JsonSerializerOptions CustomJsonSerializerOptions { get; init; } = new()
		{
			Converters = { new SmartBoolJsonConverter() }
		};

		public WeatherApi(Config config) => ApiKey = config.WApiKey ?? throw new UserException("Missing API key.");

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
					.Append(Uri.EscapeDataString(value));

			HttpWebRequest request = WebRequest.CreateHttp(sb.ToString());

			WebResponse response;
			try { response = request.GetResponse(); }
			catch (WebException ex) { response = ex.Response; } // 4xx responses still contain useful error info in form of json

			using var stream = response.GetResponseStream();
			using var reader = new StreamReader(stream);

			string json = reader.ReadToEnd();

			if (json.StartsWith(@"{""error"":{"))
				throw new WeatherApiErrorException(json);

			response.Dispose();
			return json;
		}

		public CurrentWeatherRoot GetCurrentWeather(string location, bool includeAirQuality = false)
		{
			string json = ApiFetch(
				ApiMethod.CurrentWeather,
				("q", location),
				("aqi", includeAirQuality ? "yes" : "no"));

			return JsonSerializer.Deserialize<CurrentWeatherRoot>(json, CustomJsonSerializerOptions);
		}

		public ForecastRoot GetForecast(string location, int days, bool includeAirQuality = false, bool includeAlerts = false)
		{
			string json = ApiFetch(
				ApiMethod.Forecast,
				("q", location),
				("days", days.ToString()),
				("aqi", includeAirQuality ? "yes" : "no"),
				("alerts", includeAlerts ? "yes" : "no"));

			return JsonSerializer.Deserialize<ForecastRoot>(json, CustomJsonSerializerOptions);
		}

		public ForecastRoot GetHistory(string location, string date, string endDate = null, bool includeAirQuality = false, bool includeAlerts = false)
		{
			string json = endDate is null
				? ApiFetch(
					ApiMethod.Histroy,
					("q", location),
					("dt", date),
					("aqi", includeAirQuality ? "yes" : "no"),
					("alerts", includeAlerts ? "yes" : "no"))
				: ApiFetch(
					ApiMethod.Histroy,
					("q", location),
					("dt", date),
					("end_dt", endDate),
					("aqi", includeAirQuality ? "yes" : "no"),
					("alerts", includeAlerts ? "yes" : "no"));

			return JsonSerializer.Deserialize<ForecastRoot>(json, CustomJsonSerializerOptions);
		}
	}
}
