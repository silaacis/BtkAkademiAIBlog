using BtkAkademiAIBlog.WebApi.Entities;

namespace BtkAkademiAIBlog.WebApi.Dtos.CommentDtos
{
    public class GetCommentByIdDto
    {
        public int CommentId { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public DateTime CommentDate { get; set; }
        public string CommentDetail { get; set; }
        public bool IsConfirm { get; set; }
        public string CommentStatus { get; set; }
        public int ArticleId { get; set; }
    }
}
