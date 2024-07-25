using BlogManagement.Application.Contracts.ArticleCategory;
using BlogManagement.Domain.ArticleCategoryAgg;
using Framework.Infrastructure;
using System.Collections.Generic;
using System.Linq;

namespace BlogManagement.Infrastructure.EFCore.Repository
{
    public class ArticleCategoryRepository : RepositoryBase<long,ArticleCategory>, IArticleCategoryRepository
    {
        private readonly BlogContext blogContext;

        public ArticleCategoryRepository(BlogContext blogContext):base(blogContext)
        {
            this.blogContext = blogContext;
        }

        public IEnumerable<ArticleCategory> Search(ArticleCategorySearchModel searchModel)
        {
            IQueryable<ArticleCategory> query = blogContext.ArticleCategories;
            if (searchModel.Name != null)
                query = query.Where(x => x.Name.Contains(searchModel.Name));
            return query.OrderByDescending(x => x.Id);

        }
    }
}
