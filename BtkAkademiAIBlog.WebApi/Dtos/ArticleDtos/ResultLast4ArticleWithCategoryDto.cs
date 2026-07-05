namespace BtkAkademiAIBlog.WebApi.Dtos.ArticleDtos
{
    public class ResultLast4ArticleWithCategoryDto
    {
        public int ArticleId { get; set; }
        public string Title { get; set; }
        public string Image300x300Url { get; set; }
        public string CategoryName { get; set; }
    }
}
