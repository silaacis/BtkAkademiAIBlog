namespace BtkAkademiAIBlog.WebUI.Dtos.ArticleDtos
{
    public class LastArticlesByCategoryDto
    {
        public int ArticleId { get; set; }
        public string CategoryName { get; set; }
        public string Title { get; set; }
        public string SliderCategoryImageUrl { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
