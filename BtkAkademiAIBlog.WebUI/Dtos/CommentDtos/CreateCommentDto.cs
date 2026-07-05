namespace BtkAkademiAIBlog.WebUI.Dtos.CommentDtos
{
    public class CreateCommentDto
    {
        public string AppUserId { get; set; }
        public DateTime CommentDate { get; set; }
        public string CommentDetail { get; set; }
        public bool IsConfirm { get; set; }
        public string CommentStatus { get; set; }
        public int ArticleId { get; set; }
    }
}
