using System.Text.Json.Serialization;

namespace WeatherFetch.Api.Data
{
	public class AirQuality
	{
		[JsonPropertyName("co")]
		public float CarbonMonoxide { get; set; }
		
		[JsonPropertyName("no2")]
		public float NitrogenDioxide { get; set; }
		
		[JsonPropertyName("o3")]
		public float Ozone { get; set; }
		
		[JsonPropertyName("so2")]
		public float SuflphurDioxide { get; set; }
		
		[JsonPropertyName("pm2_5")]
		public float ParticulateMatter2_5 { get; set; }
		
		[JsonPropertyName("pm10")]
		public float ParticulateMatter10 { get; set; }

		[JsonPropertyName("us-epa-index")]
		public int USEpaIndex { get; set; }

		[JsonPropertyName("gb-defra-index")]
		public int UKDefraIndex { get; set; }
	}
}