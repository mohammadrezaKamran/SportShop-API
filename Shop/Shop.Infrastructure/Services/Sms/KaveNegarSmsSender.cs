using Common.Application.Validation;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Infrastructure.Services.Sms
{
    internal class KaveNegarSmsSender:ISmsSender
    {
      
        private readonly string _apiKey;

        public KaveNegarSmsSender(IConfiguration configuration)
        {
            _apiKey = configuration["SmsSettings:ApiKey"];
        }

        public async Task SendSmsAsync(string phoneNumber, string message)
        {
            using var client = new HttpClient();
            var values = new Dictionary<string, string>
        {
            { "receptor", phoneNumber },
            { "message", message },
            { "sender", "1000XXXX" }, // شماره فرستنده
            { "apikey", _apiKey }
        };

            var content = new FormUrlEncodedContent(values);
            var response = await client.PostAsync("https://api.kavenegar.com/v1/yourapikey/sms/send.json", content);
            response.EnsureSuccessStatusCode();
        }
    }
}
