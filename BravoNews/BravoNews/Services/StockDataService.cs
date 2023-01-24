using BravoNews.Models;

namespace BravoNews.Services
{
    public class StockDataService : IStockDataService
    {
        private const string BaseUrl = "https://stockapinewsapp.azurewebsites.net";
        private readonly HttpClient _http;

        public StockDataService(HttpClient http)
        {
            _http = http;
        }

        public IEnumerable<StockData> GetData(string region = "US")
        {
            var response = _http.GetAsync($"{BaseUrl}/Summary?region={region}").Result;
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var data = response.Content.ReadFromJsonAsync<StockDataTop10>().Result;
            return data.Top10;
        }
    }
}
