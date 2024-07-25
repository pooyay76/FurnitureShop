using BlogManagement.Application.Contracts.ArticleCategory;
using Framework.Domain;
using System.Collections.Generic;

namespace BlogManagement.Domain.ArticleCategoryAgg
{
    public interface IArticleCategoryRepository : IRepository<long, ArticleCategory>
    {
        IEnumerable<ArticleCategory> Search(ArticleCategorySearchModel searchModel);
    }
}
