using System.Text;
using System.Text.Json;

namespace BtkAkademiAIBlog.WebUI.Services
{
    public class OpenAIArticleTitleService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public OpenAIArticleTitleService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task<string> GenerateArticleTitleAsync(string topic)
        {
            var apiKey = _configuration["Groq:ApiKey"];

            var client = _httpClientFactory.CreateClient();

            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

            var requestBody = new
            {
                model = "llama-3.3-70b-versatile",
                messages = new[]
                {
            new { role = "system", content = "Sen profesyonel bir makale başlık oluşturucususun." },
            new
            {
                role = "user",
                content = $"'{topic}' makale anahtar kelimelerini referans alarak bu konuya uygun 3 adet farklı makale başlığı önermeni istiyorum. Başka ekleme yapma sadece başlıkları ver."
            }
        },
                temperature = 0.6,
                max_tokens = 700
            };

            var content = new StringContent(
                JsonSerializer.Serialize(requestBody),
                Encoding.UTF8,
                "application/json");

            var response = await client.PostAsync(
                "https://api.groq.com/openai/v1/chat/completions",
                content);

            var responseString = await response.Content.ReadAsStringAsync();

            using var doc = JsonDocument.Parse(responseString);

            return doc.RootElement
                      .GetProperty("choices")[0]
                      .GetProperty("message")
                      .GetProperty("content")
                      .GetString();
        }
    }
}
