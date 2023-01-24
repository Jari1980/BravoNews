using BravoNews.Models;

namespace BravoNews.Services
{
    public interface IStockDataService
    {
        IEnumerable<StockData> GetData(string region = "US");
    }
}
