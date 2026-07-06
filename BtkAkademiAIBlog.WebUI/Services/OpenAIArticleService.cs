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
            var apiKey = _configuration["OpenAI:ApiKey"];

            if (string.IsNullOrWhiteSpace(apiKey) || apiKey.Contains("fake"))
            {
                return $@"
                    {topic} Hakkında Demo Makale

                    {topic}, günümüzde dikkat çeken ve birçok alanda etkisini gösteren önemli konulardan biridir. Bu konu hakkında yapılan çalışmalar, hem bireysel hem de toplumsal açıdan farklı bakış açıları sunmaktadır.

                    Giriş bölümünde {topic} kavramının temel olarak ne anlama geldiği açıklanabilir. Bu kavram, gelişen teknoloji, değişen yaşam alışkanlıkları ve bilgiye erişimin kolaylaşmasıyla birlikte daha fazla önem kazanmıştır.

                    Gelişme bölümünde ise {topic} konusunun avantajları, kullanım alanları ve gelecekte sağlayabileceği katkılar ele alınabilir. Özellikle dijitalleşmenin hızlanmasıyla birlikte bu alanda yapılan yenilikler, insanların hayatını kolaylaştırmakta ve yeni fırsatlar oluşturmaktadır.

                    Sonuç olarak {topic}, üzerinde durulması gereken değerli bir konudur. Doğru şekilde ele alındığında hem bireylere hem de kurumlara önemli katkılar sağlayabilir.

                    Not: Bu içerik demo olarak oluşturuldu. OpenAI API key aktif olmadığı için gerçek yapay zeka cevabı yerine örnek yanıt döndürüldü.";
            }

            var client = _httpClientFactory.CreateClient();

            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

            var requestBody = new
            {
                model = "gpt-4o-mini",
                messages = new[]
                {
            new { role = "system", content = "Sen profesyonel bir makale yazarısın." },
            new
            {
                role = "user",
                content = $"'{topic}' konusu hakkında giriş, gelişme ve sonuç içeren, " +
                          $"SEO uyumlu, akademik ama aynı zamanda kısmen samimi tonlu, minimum 1500 karakter uzunluğunda " +
                          $"detaylı bir makale yaz."
            }
        },
                temperature = 0.7,
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
