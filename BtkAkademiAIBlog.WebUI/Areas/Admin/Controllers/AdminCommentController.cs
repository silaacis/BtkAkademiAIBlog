using BtkAkademiAIBlog.WebUI.Dtos.CommentDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;


namespace BtkAkademiAIBlog.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminCommentController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public AdminCommentController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }
        public async Task<IActionResult> CommentList()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7076/api/Comments/CommentListWithArticleAndAuthor");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultCommentDto>>(jsonData);
                return View(values);
            }
            return View();
        }

        [HttpGet]
        public IActionResult CreateComment()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateComment(string CommentDetail)
        {
            try
            {
                string turkishText = CommentDetail;
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
                            content = $"Translate this Turkish text to English. Only provide the translation, nothing else: {turkishText}"
                        }
                    },
                    max_tokens = 200,
                    temperature = 0.1,
                };

                
                var jsonContent = System.Text.Json.JsonSerializer.Serialize(requestData);
                var content  = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync(apiUrl, content);
                var responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var result = System.Text.Json.JsonSerializer.Deserialize<ChatCompletionResponse>(responseContent);

                    var translatedText = result?.choices?[0]?.message?.content?.Trim();

                    ViewBag.Success = true;
                    ViewBag.OriginalText = turkishText;
                    ViewBag.TranslatedText = translatedText;
                    ViewBag.Model = "meta-llama/Llama-3.1-8B-Instruct";
                    ViewBag.StatusCode = (int)response.StatusCode;

                    return View();  
                }
                else
                {
                    ViewBag.Success = false;
                    ViewBag.Error = $"API hatası: {response.StatusCode}";
                    ViewBag.Details = responseContent;
                    ViewBag.OriginalText = turkishText;

                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewBag.Success = false; 
                ViewBag.Error = "Bir hata oluştu.";
                ViewBag.Message = ex.Message;

                return View();
            }
        }

        //[HttpGet]
        //public IActionResult Test()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public async Task<IActionResult> Test()
        //{

        //}
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
            public string role { get; set; }
            public string content { get; set; }
        }

    }
}
