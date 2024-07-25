using BlogManagement.Application.Contracts.Article;
using Framework.Domain;
using System.Collections.Generic;

namespace BlogManagement.Domain.ArticleAgg
{
    public interface IArticleRepository : IRepository<long,Article>
    {
        public List<ArticleViewModel> Search(ArticleSearchModel searchModel);
    }
}
