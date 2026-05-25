using BtkAkademiAIBlog.WebUI.Dtos.ArticleDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BtkAkademiAIBlog.WebUI.Controllers
{
    public class ArticleController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ArticleController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public IActionResult ArticleDetail()
        {
            return View();
        }

        public async Task<IActionResult> ArticleList()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7076/api/Articles");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultArticleDto>>(jsonData);
                 return View(values);
            }
            return View();
        }

        public async Task<IActionResult> ArticleList2()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7076/api/Articles");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultArticleDto>>(jsonData);
                return View(values);
            }
            return View();
        }

        public IActionResult ArticleListByCategory()
        {
            return View();
        }
    }
}
