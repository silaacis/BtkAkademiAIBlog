namespace BtkAkademiAIBlog.WebUI.Dtos.TradingVideoDtos
{
    public class ResultTradingVideoDto
    {
        public int TradingVideoId { get; set; }
        public string Title { get; set; }
        public string ThumbnailImageUrl { get; set; }
        public DateTime CreatedDate { get; set; }
        public string EmbedVideoUrl { get; set; }
        public bool IsFeature { get; set; }
        public string? FeatureImageUrl { get; set; }
    }
}
