using BravoNews.Models;

namespace BravoNews.Services
{
    public class EmailService : IEmailService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        public EmailService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClient = httpClientFactory.CreateClient("sendemail");
            _configuration = configuration;
        }

        public async Task<string> SendSubscriptionEmail(Email newEmail)
        {
            var responsMessage = await _httpClient.PostAsJsonAsync(_configuration["AzureRequestAddress"], newEmail);
            if (!responsMessage.IsSuccessStatusCode)
            {
                return "Some error occured";
            }
            return "Email will be sent";
        }
        public async Task<string> SendRegisterEmail(Email newEmail)
        {
            var responsMessage = await _httpClient.PostAsJsonAsync(_configuration["AzureRequestAddress"], newEmail);
            if (!responsMessage.IsSuccessStatusCode)
            {
                return "Some error occured";
            }
            return "Email will be sent";
        }
    }
}
