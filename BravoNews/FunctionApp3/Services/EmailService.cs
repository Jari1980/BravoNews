using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using TimerNewsApp.Model;

namespace TimerNewsApp.Services
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
