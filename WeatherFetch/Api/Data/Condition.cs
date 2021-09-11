using System.Text.Json.Serialization;

namespace WeatherFetch.Api.Data
{
	public class Condition
	{
		[JsonPropertyName("text")]
		public string Text { get; set; }

		[JsonPropertyName("icon")]
		public string IconLink { get; set; }

		[JsonPropertyName("code")]
		public int Code { get; set; }
	}
}
