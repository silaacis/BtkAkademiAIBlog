using BtkAkademiAIBlog.WebUI.Dtos.ArticleDtos;
using BtkAkademiAIBlog.WebUI.Dtos.CategoryDtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Text;

namespace BtkAkademiAIBlog.WebUI.Controllers
{
    public class AdminArticleController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public AdminArticleController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
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

        [HttpGet]
        public async Task<IActionResult> CreateArticle()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7076/api/Categories");
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultCategoryDto>>(jsonData);

            List<SelectListItem> categoryValues = (from x in values
                                                   select new SelectListItem
                                                   {
                                                       Text = x.CategoryName,
                                                       Value = x.CategoryId.ToString()
                                                   }).ToList();
            ViewBag.CategoryValues = categoryValues;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateArticle(CreateArticleDto createArticleDto)
        {
            createArticleDto.CreatedDate = DateTime.Now;
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createArticleDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            await client.PostAsync("https://localhost:7076/api/Articles", stringContent);
            return RedirectToAction("ArticleList");

        }
        public async Task<IActionResult> DeleteArticle(int id)
        {
            var client = _httpClientFactory.CreateClient();
            await client.DeleteAsync($"https://localhost:7076/api/Articles?id=" + id);
            return RedirectToAction("ArticleList");
        }
        [HttpGet]
        public async Task<IActionResult> UpdateArticle(int id)
        {
            var client1 = _httpClientFactory.CreateClient();
            var responseMessage1 = await client1.GetAsync("https://localhost:7076/api/Categories");
            var jsonData1 = await responseMessage1.Content.ReadAsStringAsync();
            var values1 = JsonConvert.DeserializeObject<List<ResultCategoryDto>>(jsonData1);

            List<SelectListItem> categoryValues = (from x in values1
                                                   select new SelectListItem
                                                   {
                                                       Text = x.CategoryName,
                                                       Value = x.CategoryId.ToString()
                                                   }).ToList();
            ViewBag.CategoryValues = categoryValues;

            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7076/api/Articles/GetArticle?id=" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<GetArticleByIdDto>(jsonData);
                return View(values);
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UpdateArticle(UpdateArticleDto updateArticleDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateArticleDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            await client.PutAsync("https://localhost:7076/api/Articles", stringContent);
            return RedirectToAction("ArticleList");
        }
    }
}
