using BravoNews.Models;
using Microsoft.AspNetCore.Mvc;




namespace BravoNews.Services
{
    public class ElectricityService : IElectricityService
    {

        private const string Url = "https://spotfunc.azurewebsites.net/api/SpotPriceRequest?code=vgUdbbCJSApniy7OgY2tfEJuTomMaNzZ-QWTNcMYS8h-AzFuS91H_w==";
        private readonly HttpClient _httpClient;

        public ElectricityService(HttpClient httpclient)
        {
            _httpClient = httpclient; 
        }

        public Electricity GetElectricityPrice()
        {

            var response = _httpClient.GetAsync(Url).Result;
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var data = response.Content.ReadFromJsonAsync<Electricity>().Result;
            return data;
        }






    }
}
