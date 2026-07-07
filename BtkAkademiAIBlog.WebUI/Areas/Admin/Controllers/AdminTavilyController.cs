using BtkAkademiAIBlog.WebUI.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace BtkAkademiAIBlog.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminTavilyController : Controller
    {   
        private readonly IConfiguration _configuration;

        public AdminTavilyController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private const string ApiUrl = "https://api.tavily.com/search";

        // GET: Search/Index
        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.SearchCompleted = false;
            return View();
        }

        // POST: Search/Index
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                ViewBag.Error = "Lütfen bir arama sorgusu girin.";
                ViewBag.SearchCompleted = false;
                return View();
            }

            try
            {
                using var httpClient = new HttpClient();

                var requestBody = new
                {
                    api_key = _configuration["Tavily:ApiKey"],
                    query = query,
                    search_depth = "advanced",
                    include_answer = true,
                    max_results = 5
                };

                var jsonContent = JsonSerializer.Serialize(requestBody);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync(ApiUrl, content);
                response.EnsureSuccessStatusCode();

                var responseContent = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonSerializer.Deserialize<TavilyApiResponse>(responseContent, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                ViewBag.Query = query;
                ViewBag.Answer = apiResponse?.Answer ?? "Cevap bulunamadı";
                ViewBag.Results = apiResponse?.Results ?? new List<TavilyResult>();
                ViewBag.SearchCompleted = true;
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Arama sırasında bir hata oluştu: {ex.Message}";
                ViewBag.SearchCompleted = false;
            }

            return View();
        }
    }


} 