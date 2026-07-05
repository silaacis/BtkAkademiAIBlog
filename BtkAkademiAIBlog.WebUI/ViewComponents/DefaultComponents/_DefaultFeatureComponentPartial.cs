using BtkAkademiAIBlog.WebUI.Dtos.ArticleDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BtkAkademiAIBlog.WebUI.ViewComponents.DefaultComponents
{
    public class _DefaultFeatureComponentPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public _DefaultFeatureComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            #region Last_Technology_Article

            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7076/api/Articles/GetLastTechnologyArticle");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<ResultArticleDto>(jsonData);
                ViewBag.LastTechnologyArticleTitle = values.Title;
                ViewBag.LastTechnologyArticleFeatureImageUrl = values.FeatureImageUrl;
                ViewBag.LastTechnologyArticleCreatedDate = values.CreatedDate;
                ViewBag.LastTechnologyArticleAuthor = values.Name + " " + values.Surname;
                ViewBag.LastTechnologyArticleImageUrl = values.ImageUrl;
            }
            #endregion

            #region Last_Travel_Article

            var client2 = _httpClientFactory.CreateClient();
            var responseMessage2 = await client2.GetAsync("https://localhost:7076/api/Articles/GetLastTravelArticle");
            if (responseMessage2.IsSuccessStatusCode)
            {
                var jsonData2 = await responseMessage2.Content.ReadAsStringAsync();
                var values2 = JsonConvert.DeserializeObject<ResultArticleDto>(jsonData2);
                ViewBag.LastTravelArticleTitle = values2.Title;
                ViewBag.LastTravelArticleFeatureImageUrl = values2.FeatureImageUrl;
                ViewBag.LastTravelArticleAuthor = values2.Name + " " + values2.Surname;
                ViewBag.LastTravelArticleImageUrl = values2.ImageUrl;
            }
            #endregion

            #region Last_Sports_Article

            var client3 = _httpClientFactory.CreateClient();
            var responseMessage3 = await client3.GetAsync("https://localhost:7076/api/Articles/GetLastSportsArticle");
            if (responseMessage3.IsSuccessStatusCode)
            {
                var jsonData3 = await responseMessage3.Content.ReadAsStringAsync();
                var values3 = JsonConvert.DeserializeObject<ResultArticleDto>(jsonData3);
                ViewBag.LastSportsArticleTitle = values3.Title;
              ViewBag.LastSportsArticleFeatureImageUrl = values3.FeatureImageUrl;
                ViewBag.LastSportsArticleAuthor = values3.Name + " " + values3.Surname;
                ViewBag.LastSportsArticleImageUrl = values3.ImageUrl;
            }
            #endregion

            return View();
        }     
    }
}
