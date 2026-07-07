using System.Text;
using System.Text.Json;

namespace BtkAkademiAIBlog.WebUI.Services
{
    public class OpenAIArticleService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public OpenAIArticleService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task<string> GenerateArticleAsync(string topic)
        {
            var apiKey = _configuration["Groq:ApiKey"];

            var client = _httpClientFactory.CreateClient();

            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

            var requestBody = new
            {
                model = "llama-3.3-70b-versatile",
                messages = new[]
        {
            new
            {
                role = "system",
                content =
                    "Sen teknoloji, yaşam, spor, finans, sanat gibi her alanda derinlemesine, akıcı ve SEO uyumlu içerik üretebilen profesyonel bir baş editör ve kıdemli içerik yazarıyısın. " +
                    "Sana verilen konunun türünü otomatik analiz et ve o türe en uygun anlatım tarzını kendin seç. " +
                    "Asla aynı cümleleri veya fikirleri tekrar etme. Temiz, akıcı ve imla kurallarına uygun Türkçe kullan."
            },
            new
            {
                role = "user",
                content =
                    $"'{topic}' konusu hakkında; okuyucuyu meraklandıran bir giriş paragrafı, " +
                    $"konuyu farklı boyutlarıyla ele alan detaylı gelişme bölümü ve vurucu bir sonuç bölümünden oluşan, " +
                    $"minimum 1500 karakter uzunluğunda kapsamlı ve özgün bir makale yaz."
            }
        },
                temperature = 0.6,
                max_tokens = 1100
            };

            var content = new StringContent(
                JsonSerializer.Serialize(requestBody),
                Encoding.UTF8,
                "application/json");

            var response = await client.PostAsync(
                "https://api.groq.com/openai/v1/chat/completions",
                content);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();

                return $@"
                    Groq API isteği başarısız oldu.

                    Status: {response.StatusCode}

                    Detay:
                    {error}";
            }

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
