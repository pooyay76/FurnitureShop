namespace BlogManagement.Application.Contracts.Article
{
    public class ArticleViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string AuthorUsername { get; set; }
        public string Picture { get; set; }
        public string ArticleCategoryName { get; set; }
    }
}
