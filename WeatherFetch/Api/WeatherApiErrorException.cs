using System;
using System.Text.Json;
using WeatherFetch.Api.Data;

namespace WeatherFetch.Api
{
	public class WeatherApiErrorException : Exception
	{
		private readonly string _message;
		public override string Message => _message;

		private readonly int _errorCode;
		public int ErrorCode => _errorCode;

		public WeatherApiErrorException(string json)
		{
			var error = JsonSerializer.Deserialize<ErrorResponse>(json).Error;
			_message = error.Message;
			_errorCode = error.Code;
		}
	}
}
