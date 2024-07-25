namespace BlogManagement.Application.Contracts.Article
{
    public class ArticleSearchModel
    {

        public string Title { get; set; }
        public string AuthorUsername { get; set; }
        public long ArticleCategoryId { get; set; }
    }
}
