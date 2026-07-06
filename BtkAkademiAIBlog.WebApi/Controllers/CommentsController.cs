using AutoMapper;
using BtkAkademiAIBlog.WebApi.Context;
using BtkAkademiAIBlog.WebApi.Dtos.CommentDtos;
using BtkAkademiAIBlog.WebApi.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BtkAkademiAIBlog.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly BlogAIContext _context;
        private readonly IMapper _mapper;

        public CommentsController(BlogAIContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult CommentList()
        {
            var values = _context.Comments.ToList();
            return Ok(values);
        }

        [HttpPost]
        public IActionResult CreateComment(CreateCommentDto createCommentDto)
        {
            var value = _mapper.Map<Comment>(createCommentDto);
            _context.Comments.Add(value);
            _context.SaveChanges();
            return Ok("Yorum başarıyla oluşturuldu.");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteComment(int id)
        {
            var Comment = _context.Comments.Find(id);
            _context.Comments.Remove(Comment);
            _context.SaveChanges();
            return Ok("Yorum başarıyla silindi.");
        }

        [HttpGet("GetComment")]
        public IActionResult GetComment(int id)
        {
            var value = _context.Comments.Find(id);
            return Ok(value);
        }

        [HttpPut]
        public IActionResult UpdateComment(UpdateCommentDto updateCommentDto)
        {
            var value = _mapper.Map<Comment>(updateCommentDto);
            _context.Comments.Update(value);
            _context.SaveChanges();
            return Ok("Yorum Güncelleme İşlemi Başarılı");
        }

        [HttpGet("CommentListWithArticleAndAuthor")]
        public IActionResult CommentListWithArticleAndAuthor()
        {
            var values = _context.Comments
                .Include(x=>x.Article)
                .Include(y=>y.AppUser)
                .ToList();

            var dtoValues = _mapper.Map<List<ResultCommentWithArticleAndAuthorDto>>(values);

            return Ok(dtoValues);
        }
    }
}
