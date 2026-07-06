using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace BtkAkademiAIBlog.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminToxicityController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public AdminToxicityController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult CheckToxicity()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CheckToxicity(string CommentText)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(CommentText))
                {
                    ViewBag.Success = false;
                    ViewBag.Error = "Lütfen analiz edilecek bir metin girin.";
                    return View();
                }

                string apiUrl = "https://router.huggingface.co/v1/chat/completions";
                var httpClient = _httpClientFactory.CreateClient();

                var hfApiToken = _configuration["HuggingFace:ApiToken"];
                httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {hfApiToken}");

                var requestData = new
                {
                    model = "meta-llama/Llama-3.1-8B-Instruct",
                    messages = new[]
                    {
                        new
                        {
                            role = "user",
                            content = $@"Analyze the toxicity of this text and respond ONLY with a valid JSON object. No explanation, no markdown, just the JSON:

Text: ""{CommentText}""

Response format:
{{
  ""toxic"": 0.0,
  ""severe_toxic"": 0.0,
  ""obscene"": 0.0,
  ""threat"": 0.0,
  ""insult"": 0.0,
  ""identity_hate"": 0.0
}}

Rate each category from 0.0 (not present) to 1.0 (definitely present)."
                        }
                    },
                    max_tokens = 300,
                    temperature = 0.1
                };

                var jsonContent = System.Text.Json.JsonSerializer.Serialize(requestData);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync(apiUrl, content);
                var responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var result = System.Text.Json.JsonSerializer.Deserialize<ChatCompletionResponse>(responseContent);
                    var generatedText = result?.choices?[0]?.message?.content?.Trim();

                    if (!string.IsNullOrEmpty(generatedText))
                    {
                        // JSON kısmını çıkar (```json``` veya ``` tagları varsa temizle)
                        var cleanedJson = generatedText
                            .Replace("```json", "")
                            .Replace("```", "")
                            .Trim();

                        // JSON başlangıç ve bitiş noktalarını bul
                        var jsonStart = cleanedJson.IndexOf('{');
                        var jsonEnd = cleanedJson.LastIndexOf('}');

                        if (jsonStart >= 0 && jsonEnd > jsonStart)
                        {
                            var jsonString = cleanedJson.Substring(jsonStart, jsonEnd - jsonStart + 1);

                            try
                            {
                                // Newtonsoft.Json kullan (JsonConvert) - daha esnek
                                var toxicityScores = JsonConvert.DeserializeObject<ToxicityScores>(jsonString);

                                if (toxicityScores != null)
                                {
                                    // En yüksek skoru bul
                                    var maxScore = Math.Max(
                                        Math.Max(toxicityScores.toxic, toxicityScores.severe_toxic),
                                        Math.Max(Math.Max(toxicityScores.obscene, toxicityScores.threat),
                                        Math.Max(toxicityScores.insult, toxicityScores.identity_hate))
                                    );

                                    // Hangi kategori en yüksek?
                                    string maxLabel = "clean";
                                    if (maxScore == toxicityScores.toxic) maxLabel = "toxic";
                                    else if (maxScore == toxicityScores.severe_toxic) maxLabel = "severe_toxic";
                                    else if (maxScore == toxicityScores.obscene) maxLabel = "obscene";
                                    else if (maxScore == toxicityScores.threat) maxLabel = "threat";
                                    else if (maxScore == toxicityScores.insult) maxLabel = "insult";
                                    else if (maxScore == toxicityScores.identity_hate) maxLabel = "identity_hate";

                                    string toxicityLevel = DetermineToxicityLevel(maxScore);
                                    string levelColor = GetLevelColor(maxScore);

                                    ViewBag.Success = true;
                                    ViewBag.OriginalText = CommentText;
                                    ViewBag.Model = "meta-llama/Llama-3.2-3B-Instruct";
                                    ViewBag.StatusCode = (int)response.StatusCode;

                                    // Detaylı skorlar
                                    ViewBag.ToxicScore = toxicityScores.toxic;
                                    ViewBag.SevereToxicScore = toxicityScores.severe_toxic;
                                    ViewBag.ObsceneScore = toxicityScores.obscene;
                                    ViewBag.ThreatScore = toxicityScores.threat;
                                    ViewBag.InsultScore = toxicityScores.insult;
                                    ViewBag.IdentityHateScore = toxicityScores.identity_hate;

                                    ViewBag.MaxScore = maxScore;
                                    ViewBag.MaxLabel = maxLabel;
                                    ViewBag.ToxicityLevel = toxicityLevel;
                                    ViewBag.LevelColor = levelColor;

                                    // Yüzdelik değerler
                                    ViewBag.ToxicPercent = Math.Round(toxicityScores.toxic * 100, 2);
                                    ViewBag.SevereToxicPercent = Math.Round(toxicityScores.severe_toxic * 100, 2);
                                    ViewBag.ObscenePercent = Math.Round(toxicityScores.obscene * 100, 2);
                                    ViewBag.ThreatPercent = Math.Round(toxicityScores.threat * 100, 2);
                                    ViewBag.InsultPercent = Math.Round(toxicityScores.insult * 100, 2);
                                    ViewBag.IdentityHatePercent = Math.Round(toxicityScores.identity_hate * 100, 2);

                                    return View();
                                }
                            }
                            catch (System.Text.Json.JsonException jsonEx)
                            {
                                ViewBag.Success = false;
                                ViewBag.Error = "JSON parse hatası";
                                ViewBag.Message = jsonEx.Message;
                                ViewBag.Details = $"Gelen JSON: {jsonString}";
                                ViewBag.OriginalText = CommentText;
                                return View();
                            }
                        }

                        ViewBag.Success = false;
                        ViewBag.Error = "Model yanıtı JSON formatında değil";
                        ViewBag.Details = generatedText;
                        ViewBag.OriginalText = CommentText;
                        return View();
                    }

                    ViewBag.Success = false;
                    ViewBag.Error = "Model boş yanıt döndü";
                    ViewBag.Details = responseContent;
                    ViewBag.OriginalText = CommentText;
                    return View();
                }
                else
                {
                    ViewBag.Success = false;
                    ViewBag.Error = $"API Hatası: {response.StatusCode}";
                    ViewBag.Details = responseContent;
                    ViewBag.OriginalText = CommentText;
                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewBag.Success = false;
                ViewBag.Error = "Bir hata oluştu";
                ViewBag.Message = ex.Message;
                ViewBag.Details = ex.StackTrace;
                return View();
            }
        }

        // Toksiklik seviyesini belirle
        private string DetermineToxicityLevel(double score)
        {
            if (score >= 0.8) return "Çok Yüksek";
            if (score >= 0.6) return "Yüksek";
            if (score >= 0.4) return "Orta";
            if (score >= 0.2) return "Düşük";
            return "Çok Düşük";
        }

        // Seviye rengini belirle
        private string GetLevelColor(double score)
        {
            if (score >= 0.8) return "danger";      // Kırmızı
            if (score >= 0.6) return "warning";     // Turuncu
            if (score >= 0.4) return "info";        // Mavi
            if (score >= 0.2) return "secondary";   // Gri
            return "success";                        // Yeşil
        }

        // Chat Completion Response Models (Çeviri ile aynı)
        public class ChatCompletionResponse
        {
            public List<Choice> choices { get; set; }
        }

        public class Choice
        {
            public Message message { get; set; }
        }

        public class Message
        {
            public string content { get; set; }
        }

        // Toxicity Scores Model
        public class ToxicityScores
        {
            [JsonProperty("toxic")]
            public double toxic { get; set; }

            [JsonProperty("severe_toxic")]
            public double severe_toxic { get; set; }

            [JsonProperty("obscene")]
            public double obscene { get; set; }

            [JsonProperty("threat")]
            public double threat { get; set; }

            [JsonProperty("insult")]
            public double insult { get; set; }

            [JsonProperty("identity_hate")]
            public double identity_hate { get; set; }
        }
    }
}


