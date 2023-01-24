using System.Text.Json.Serialization;


namespace BravoNews.Models
{
    public class StockDataTop10
    {
        [JsonPropertyName("top10")]
        public StockData[] Top10 { get; set; }
    }
}
