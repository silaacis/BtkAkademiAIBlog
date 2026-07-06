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
            var apiKey = _configuration["OpenAI:ApiKey"];

            if (string.IsNullOrWhiteSpace(apiKey) || apiKey.Contains("fake"))
            {
                return $@"
                    {topic} Hakkında Bilmeniz Gerekenler

                    {topic} Neden Günümüzde Bu Kadar Önemli?

                    {topic} Alanında Öne Çıkan Yeni Gelişmeler

                    Yeni Başlayanlar İçin {topic} Rehberi

                    {topic} Geleceğimizi Nasıl Şekillendiriyor?

                    {topic} ile İlgili En Çok Merak Edilenler

                    {topic} Konusunda Dikkat Edilmesi Gereken 5 Nokta

                    Not: Bu başlıklar demo olarak oluşturuldu. OpenAI API key aktif olmadığı için gerçek yapay zeka cevabı yerine örnek yanıt döndürüldü.";
            }

            var client = _httpClientFactory.CreateClient();

            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

            var requestBody = new
            {
                model = "gpt-4o-mini",
                messages = new[]
                {
            new { role = "system", content = "Sen profesyonel bir makale başlık oluşturucususun." },
            new
            {
                role = "user",
                content = $"'{topic}' makale anahtar kelimelerini referans alarak bu konuya uygun bir makale başlığı önermeni istiyorum."
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
                "https://api.openai.com/v1/chat/completions",
                content);

            if (!response.IsSuccessStatusCode)
            {
                return $"{topic} hakkında demo makale oluşturuldu. OpenAI API isteği başarısız olduğu için örnek içerik gösteriliyor.";
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
