using System.Text.Json.Serialization;

namespace MyWeatherApp.Models
{

	public class GeocodeMapLocation
	{
		/*public GeocodeMapLocation(string lat, string lon, string locName)
		{
			latitude = lat;
			longitude = lon;
			location = locName;
		} */

		[JsonPropertyName("lat")]
		public string latitude { get; set; }

		[JsonPropertyName("lon")]
		public string longitude { get; set; }

		[JsonPropertyName("display_name")]
		public string location { get; set; }
	}

}

