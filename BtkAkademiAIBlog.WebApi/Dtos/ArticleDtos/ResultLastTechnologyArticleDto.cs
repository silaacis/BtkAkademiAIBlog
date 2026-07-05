namespace BtkAkademiAIBlog.WebApi.Dtos.ArticleDtos
{
    public class ResultLastTechnologyArticleDto
    {
        public int ArticleId { get; set; }
        public string Title { get; set; }
        public string FeatureImageUrl { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string ImageUrl { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
