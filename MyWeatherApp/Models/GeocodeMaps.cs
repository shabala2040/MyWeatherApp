using System.Text.Json.Serialization;

namespace MyWeatherApp.Models
{

	public class GeocodeMapLocation
	{

		[JsonPropertyName("lat")]
		public string latitude { get; set; }

		[JsonPropertyName("lon")]
		public string longitude { get; set; }

		[JsonPropertyName("display_name")]
		public string location { get; set; }
	}

}

