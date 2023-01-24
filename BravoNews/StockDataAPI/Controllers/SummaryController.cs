using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockDataAPI.Models;

namespace StockDataAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SummaryController : ControllerBase
    {
        private const int CACHE_DURATION = 60 * 60 * 24;
        private const string API_KEY = "";
        private const string API_HOST = "";

        [HttpGet(Name = "GetSummary")]
        [ResponseCache(VaryByQueryKeys = new string[] { "region" }, Duration = CACHE_DURATION)]
        public async Task<IndexSummary> GetAsync(string region)
        {
            HttpClient Client = new();


        }

        

    }
}
