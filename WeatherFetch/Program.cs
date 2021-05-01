using System;
using System.Net;

namespace WeatherFetch
{
	class Program
	{
		static void Main(string[] args)
		{
			string apiKey = System.IO.File.ReadAllText("apikey.txt");

			HttpWebRequest request = WebRequest.CreateHttp("http://api.weatherapi.com/v1/current.json?");
		}
	}
}
