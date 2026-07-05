namespace BtkAkademiAIBlog.WebApi.Entities
{
    public class TradingVideo
    {
        public int TradingVideoId { get; set; }
        public string Title { get; set; }
        public string ThumbnailImageUrl { get; set; }
        public DateTime CreatedDate { get; set; }
        public string EmbedVideoUrl { get; set; }
        public bool IsFeature { get; set; }
        public string? FeatureImageUrl { get; set; }
        public int? CategoryId { get; set; }
        public Category Category { get; set; }
        public string? AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
