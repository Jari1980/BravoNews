using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BravoNews.Models

{
    public class Electricity
    {
        [JsonPropertyName("todaysSpotPrices")]
        public IEnumerable<TodaySpotPrice> TodaysSpotPrices { get; set; }
    }

    public class TodaySpotPrice
    {
        [JsonPropertyName("spotData")]
        public IEnumerable<SpotData> SpotData { get; set; }
    }

    public class SpotData
    {
        public int Id { get; set; }

        [JsonPropertyName("dateAndTime")]
        public DateTime DateAndTime { get; set; }

        [JsonPropertyName("areaName")]
        public string AreaName { get; set; }

        [JsonPropertyName("price")]
        
        public string Price { get; set; }   

    }
}
