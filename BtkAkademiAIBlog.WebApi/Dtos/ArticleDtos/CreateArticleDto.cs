namespace BtkAkademiAIBlog.WebApi.Dtos.ArticleDtos
{
    public class CreateArticleDto
    {
        public string Title { get; set; }
        public string CoverImageUrl { get; set; }
        public string MainImageUrl { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CategoryId { get; set; }
        public bool IsFeatureSlider { get; set; }
        public string FeatureSliderImageUrl { get; set; }
        public string FeatureImageUrl { get; set; }
        public string AppUserId { get; set; }
    }
}
