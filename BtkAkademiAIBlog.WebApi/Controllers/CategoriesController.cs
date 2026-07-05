using AutoMapper;
using BtkAkademiAIBlog.WebApi.Context;
using BtkAkademiAIBlog.WebApi.Dtos.CategoryDtos;
using BtkAkademiAIBlog.WebApi.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BtkAkademiAIBlog.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly BlogAIContext _context;
        private readonly IMapper _mapper;

        public CategoriesController(BlogAIContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult CategoryList()
        {
            var values = _context.Categories.ToList(); 
            return Ok(values);
        }

        [HttpPost]
        public IActionResult CreateCategory(CreateCategoryDto createCategoryDto)
        {
            var value = _mapper.Map<Category>(createCategoryDto);
            _context.Categories.Add(value);
            _context.SaveChanges(); 
            return Ok("Kategori başarıyla oluşturuldu.");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int id)
        {
            var category = _context.Categories.Find(id);
            _context.Categories.Remove(category);
            _context.SaveChanges();
            return Ok("Kategori başarıyla silindi.");
        }

        [HttpGet("GetCategory")]
        public IActionResult GetCategory(int id)
        {
            var value = _context.Categories.Find(id);
            return Ok(value);
        }

        [HttpPut]
        public IActionResult UpdateCategory(UpdateCategoryDto updateCategoryDto)
        { 
            var value = _mapper.Map<Category>(updateCategoryDto);
            _context.Categories.Update(value);
            _context.SaveChanges();
            return Ok("Kategori Güncelleme İşlemi Başarılı");
        }

    }
}
