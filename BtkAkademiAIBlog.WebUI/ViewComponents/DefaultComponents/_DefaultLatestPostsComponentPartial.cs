using BtkAkademiAIBlog.WebUI.Dtos.ArticleDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BtkAkademiAIBlog.WebUI.ViewComponents.DefaultComponents
{
    public class _DefaultLatestPostsComponentPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public _DefaultLatestPostsComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7076/api/Articles/GetLastArticle");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<ResultLastArticleDto>(jsonData);

                if (values == null)
                {
                    return View();
                }

                ViewBag.Title = values.Title;
                ViewBag.LastArticleImageUrl = values.LastArticleImageUrl;
                ViewBag.NameSurname = values.Name + " " + values.Surname;
                ViewBag.CreatedDate = values.CreatedDate.ToString("dd MMMM yyyy");
                ViewBag.CategoryName = values.CategoryName;

                return View();
            }
            return View();
        }
    }
}
