using System;

namespace WeatherFetch.Api
{
	// just to differentiate exceptions caused by the user (and to give them a nice and meaningul message)
	// and exceptions caused by bugs in the system (printing a full stack trace for easy debugging)
	public class UserException : Exception
	{
		public UserException(string message) : base(message) { }
	}
}
