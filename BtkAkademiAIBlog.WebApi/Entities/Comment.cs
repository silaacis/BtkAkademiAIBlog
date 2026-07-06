namespace BtkAkademiAIBlog.WebApi.Entities
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public DateTime CommentDate { get; set; }
        public string CommentDetail { get; set; }
        public bool IsConfirm { get; set; }
        public string CommentStatus { get; set; }
        public int ArticleId { get; set; }
        public Article Article { get; set; }
        public decimal Rating { get; set; }
    }
}
