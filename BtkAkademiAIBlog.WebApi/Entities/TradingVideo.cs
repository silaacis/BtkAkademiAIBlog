namespace BtkAkademiAIBlog.WebApi.Entities
{
    public class TradingVideo
    {
        public int TradingVideoId { get; set; }
        public string Title { get; set; }
        public string ThumbnailImageUrl { get; set; }
        public DateTime CreatedDate { get; set; }
        public string EmbedVideoUrl { get; set; }
    }
}
