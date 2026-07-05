using BtkAkademiAIBlog.WebApi.Context;
using BtkAkademiAIBlog.WebApi.Dtos.TradingVideoDtos;
using BtkAkademiAIBlog.WebApi.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BtkAkademiAIBlog.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TradingVideosController : ControllerBase
    {
        private readonly BlogAIContext _context;

        public TradingVideosController(BlogAIContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult TradingVideoList()
        {
            var values = _context.TradingVideos.ToList();
            return Ok(values);
        }

        [HttpGet("GetTradingFeatureVideo")]
        public IActionResult GetTradingFeatureVideo()
        {
            var value = _context.TradingVideos
                .Where(x => x.IsFeature == true)
                .Include(x => x.AppUser)
                .Select(a=>new GetTradingVideoFeatureByTrueDto
                {
                    CreatedDate = a.CreatedDate,
                    EmbedVideoUrl = a.EmbedVideoUrl, 
                    FeatureImageUrl = a.FeatureImageUrl,
                    Title = a.Title,
                    IsFeature = a.IsFeature,
                    ThumbnailImageUrl = a.ThumbnailImageUrl,
                    TradingVideoId = a.TradingVideoId,
                    UserNameSurname = a.AppUser.Name + " " + a.AppUser.Surname,
                    UserImageUrl = a.AppUser.ImageUrl
                }).FirstOrDefault();
            return Ok(value);
        }

        [HttpPost]
        public IActionResult CreateTradingVideo(TradingVideo tradingVideo)
        {
            _context.TradingVideos.Add(tradingVideo);
            _context.SaveChanges();
            return Ok("Ekleme işlemi başarılı.");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTradingVideo(int id)
        {
            var tradingVideo = _context.TradingVideos.Find(id);
            _context.TradingVideos.Remove(tradingVideo);
            _context.SaveChanges();
            return Ok("Silme İşlemi Başarılı!");
        }

        [HttpGet("GetTradingVideo")]
        public IActionResult GetTradingVideo(int id)
        {
            var value = _context.TradingVideos.Find(id);
            return Ok(value);
        }

        [HttpPut]
        public IActionResult UpdateTradingVideo(TradingVideo tradingVideo)
        {
            _context.TradingVideos.Update(tradingVideo);
            _context.SaveChanges();
            return Ok("Güncelleme İşlemi Başarılı");
        }
    }
}
