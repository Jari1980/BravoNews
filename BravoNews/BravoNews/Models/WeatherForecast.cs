using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BravoNews.Models
{
    public class WeatherIcon
    {
        [JsonPropertyName("code")]
        public string Code { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }
    }

    public class WeatherForecast
    {
        [JsonPropertyName("summary")]
        public string Summary { get; set; }

        [JsonPropertyName("temperatureC")]
        public int TemperatureC { get; set; }

        [JsonPropertyName("temperatureF")]
        public int TemperatureF { get; set; }

        [JsonPropertyName("humidity")]
        public int Humidity { get; set; }

        [JsonPropertyName("windSpeed")]
        public int WindSpeed { get; set; }

        [JsonPropertyName("icon")]
        public WeatherIcon Icon { get; set; }

        [JsonPropertyName("date")]
        public DateTime Date { get; set; }

        [JsonPropertyName("city")]
        public string City { get; set; }
    }
}

