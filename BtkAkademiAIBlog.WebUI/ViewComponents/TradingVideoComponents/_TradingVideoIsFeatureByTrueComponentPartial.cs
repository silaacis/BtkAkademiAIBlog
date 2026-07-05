using BtkAkademiAIBlog.WebUI.Dtos.ArticleDtos;
using BtkAkademiAIBlog.WebUI.Dtos.TradingVideoDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BtkAkademiAIBlog.WebUI.ViewComponents.TradingVideoComponents
{
    public class _TradingVideoIsFeatureByTrueComponentPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public _TradingVideoIsFeatureByTrueComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7076/api/TradingVideos/GetTradingFeatureVideo");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<GetTradingFeatureVideo>(jsonData);
                ViewBag.Title = values.Title;
                ViewBag.FeatureImageUrl = values.FeatureImageUrl;
                ViewBag.EmbedVideoUrl = values.EmbedVideoUrl;
                ViewBag.CreatedDate = values.CreatedDate;
                ViewBag.UserNameSurname = values.UserNameSurname;
                ViewBag.UserImageUrl = values.UserImageUrl;
            }
            return View();
        }
    }
}
