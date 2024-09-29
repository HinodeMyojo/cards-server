using CardsServer.BLL.Abstractions;

namespace CardsServer.BLL.Services.Email
{
    public class EmailService : IEmailService
    {
        private readonly HttpClient _httpClient;

        public EmailService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task SendEmailAsync(List<string> to, string subject, string body)
        {
            var emailRequest = new
            {
                To = to,
                Subject = subject,
                Body = body
            };

            var response = await _httpClient.PostAsJsonAsync("тут должен быть адресс", emailRequest);

            response.EnsureSuccessStatusCode();
        }
    }
}
