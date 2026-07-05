using BtkAkademiAIBlog.WebUI.Dtos.ArticleDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BtkAkademiAIBlog.WebUI.ViewComponents.DefaultComponents
{
    public class _DefaultLast4ArticlesWithCategoryComponentPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public _DefaultLast4ArticlesWithCategoryComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7076/api/Articles/GetLast4ArticlesWithCategory");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultLast4ArticleWithCategoryDto>>(jsonData);
                return View(values);
            }
            return View();
        }
    }
}
