namespace BtkAkademiAIBlog.WebUI.Dtos.TradingVideoDtos
{
    public class GetTradingFeatureVideo
    {
        public int TradingVideoId { get; set; }
        public string Title { get; set; }
        public string ThumbnailImageUrl { get; set; }
        public DateTime CreatedDate { get; set; }
        public string EmbedVideoUrl { get; set; }
        public bool IsFeature { get; set; }
        public string? FeatureImageUrl { get; set; }
        public string UserNameSurname { get; set; }
        public string UserImageUrl { get; set; }
    }
}
