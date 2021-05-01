using System;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Text;

namespace WeatherFetch.Api
{
	public class WeatherApi
	{
		private const string BaseUrl = "https://api.weatherapi.com/v1/";
		private const string CurrentWeatherApiMethod = "current.json";

		private string ApiKey { get; init; }

		public WeatherApi(string apiKey)
		{
			ApiKey = apiKey;
		}

		public string GetCurrentWeather(string location) => ApiFetch(CurrentWeatherApiMethod, ("q", location));

		/// <summary>
		/// Base utility method for making API calls.
		/// </summary>
		/// <returns></returns>
		private string ApiFetch(string apiMethod, params (string, string)[] parameters)
		{
			var sb = new StringBuilder($"{BaseUrl}{apiMethod}?key={ApiKey}");

			foreach ((string key, string value) in parameters)
			{
				sb.Append('&');
				sb.Append(key);
				sb.Append('=');
				sb.Append(value);
			}

			HttpWebRequest request = WebRequest.CreateHttp(sb.ToString());

			using var response = request.GetResponse();
			using var stream = response.GetResponseStream();
			using var reader = new StreamReader(stream);

			return reader.ReadToEnd();
		}
	}
}
