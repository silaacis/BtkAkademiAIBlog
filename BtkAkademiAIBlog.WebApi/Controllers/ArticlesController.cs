using AutoMapper;
using BtkAkademiAIBlog.WebApi.Context;
using BtkAkademiAIBlog.WebApi.Dtos.ArticleDtos;
using BtkAkademiAIBlog.WebApi.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BtkAkademiAIBlog.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private readonly BlogAIContext _context;
        private readonly IMapper _mapper;

        public ArticlesController(BlogAIContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult ArticleList()
        {
            var values = _context.Articles.Include(x=>x.Category).ToList();
           var dto = _mapper.Map<List<ResultArticleWithCategoryDto>>(values);
            return Ok(dto);
        }

        [HttpPost]
        public IActionResult CreateArticle(CreateArticleDto createArticleDto)
        {
            createArticleDto.CreatedDate = DateTime.Now;
            var values = _mapper.Map<Article>(createArticleDto);
            _context.Articles.Add(values);
            _context.SaveChanges();
            return Ok("Ekleme işlemi başarılı");
        }

        [HttpDelete]
        public IActionResult DeleteArticle(int id)
        {
            var value = _context.Articles.Find(id);
            _context.Articles.Remove(value);
            _context.SaveChanges();
            return Ok("Silme işlemi başarılı");
        }

        [HttpGet("GetArticle")]
        public IActionResult GetArticle(int id)
        {
            var value = _context.Articles.Find(id);
            return Ok(_mapper.Map<GetArticleByIdDto>(value));
        }

        [HttpPut]
        public IActionResult UpdateArticle(UpdateArticleDto updateArticleDto)
        {
            var value = _mapper.Map<Article>(updateArticleDto);
            _context.Articles.Update(value);
            _context.SaveChanges();
            return Ok("Güncelleme işlemi başarılı");
        }

        [HttpGet("GetArticlesFeatureSliderByTrue")]
        public IActionResult GetArticlesFeatureSliderByTrue()
        {
            var values = _context.Articles
                .Where(y=>y.IsFeatureSlider == true)
                .Include(x => x.Category).ToList();
            return Ok(_mapper.Map<List<ResultArticleWithCategoryDto>>(values));
        }

        [HttpGet("GetLastTechnologyArticle")]
        public IActionResult GetLastTechnologyArticle()
        {
            var categoryId = _context.Categories.Where(x => x.CategoryName == "Teknoloji")
                .Select(y=>y.CategoryId).FirstOrDefault();
            var values = _context.Articles.Where(x => x.CategoryId == categoryId).OrderByDescending(y => y.ArticleId).Take(1).FirstOrDefault();
            return Ok(values);
        }

        [HttpGet("GetLastSportsArticle")]
        public IActionResult GetLastSportsArticle()
        {
            var categoryId = _context.Categories.Where(x => x.CategoryName == "Spor")
                .Select(y => y.CategoryId).FirstOrDefault();
            var values = _context.Articles.Where(x => x.CategoryId == categoryId).OrderByDescending(y => y.ArticleId).Take(1).FirstOrDefault();
            return Ok(values);
        }

        [HttpGet("GetLastTravelArticle")]
        public IActionResult GetLastTravelArticle()
        {
            var categoryId = _context.Categories.Where(x => x.CategoryName == "Seyahat")
                .Select(y => y.CategoryId).FirstOrDefault();
            var values = _context.Articles.Where(x => x.CategoryId == categoryId).OrderByDescending(y => y.ArticleId).Take(1).FirstOrDefault();
            return Ok(values);
        }
    }
}
