using System.Text.Json.Serialization;

namespace WeatherFetch.Api.Data
{

	internal class ErrorResponse
	{
		[JsonPropertyName("error")]
		public Error Error { get; set; }
	}

	internal class Error
	{
		[JsonPropertyName("code")]
		public int Code { get; set; }

		[JsonPropertyName("message")]
		public string Message { get; set; }
	}

}
