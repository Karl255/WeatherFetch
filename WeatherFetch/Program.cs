using System;
using System.Net;

namespace WeatherFetch
{
	class Program
	{
		static void Main(string[] args)
		{
			HttpWebRequest request = WebRequest.CreateHttp("http://api.weatherapi.com/v1/current.json?");
		}
	}
}
