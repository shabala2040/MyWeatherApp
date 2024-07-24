using System.Text.Json.Serialization;

namespace MyWeatherApp.Models
{
    
    // I implemented these classes manually based only on the fields I needed
    // It might be better to utilize something like json2csharp.com, but I chose
    // this method to avoid overcrowding this file with a lot of unnecessary data
    // that I didn't need.


    /// <summary>
    /// Represents a National Weather Service Properties object from the JSON of
    /// the NWS API. The singular variable contains a NWSPoint object.
    /// </summary>
    public class NWSPointProperties
    {
        [JsonPropertyName("forecast")]
        public string forecast { get; set; }
        [JsonPropertyName("forecastHourly")]
        public string hourlyForecast { get; set; }

    }

    /// <summary>
    /// Represents a National Weather Service forecast object from the JSON of
    /// the NWS API. The singular variable contains a NWSForecastProperties object.
    /// </summary>
    public class NWSForecast
    {
        public NWSForecastProperties properties { get; set; }
    }

    /// <summary>
    /// Represents a National Weather Service forecast properties object from the
    /// JSON of the NWS API. The singular variable holds a list of time periods
    /// that represent different forecasts.
    /// </summary>
    public class NWSForecastProperties
    {
        public NWSForecastPeriod[] periods { get; set; }
    }

    /// <summary>
    /// Represents a National Weather Service forecast period object from the
    /// JSON of the NWS API. The variables hold the necessary data for our page to
    /// show a variety of forecast details.
    /// </summary>
    public class NWSForecastPeriod
    {
        public string name { get; set; }
        public int temperature { get; set; }
        public NWSProbabilityOfPercipitation probabilityOfPercipitation { get; set; }
        public string windSpeed { get; set; }
        public string windDirection { get; set; }
        public string shortForecast { get; set; }
        public string detailedForecast { get; set; }

    }

    /// <summary>
    /// Represents the National Weather Service probability of percipitation object
    /// from the JSON of the NWS API. The variable holds the percentage possibility
    /// of rain. 
    /// </summary>
    public class NWSProbabilityOfPercipitation
    {
        [JsonPropertyName("value")]
        public int probability { get; set; }
    }
    }



