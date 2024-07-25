using BlogManagement.Application;
using BlogManagement.Application.Contracts.ArticleCategory;
using BlogManagement.Domain.ArticleCategoryAgg;
using BlogManagement.Infrastructure.EFCore;
using BlogManagement.Infrastructure.EFCore.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BlogManagement.Infrastructure.Configurations
{
    public static class BlogManagementBootstrapper
    {
        public static void Configure(IServiceCollection serviceCollection, string connectionString)
        {
            serviceCollection.AddTransient<IArticleCategoryApplication, ArticleCategoryApplication>();
            serviceCollection.AddTransient<IArticleCategoryRepository, ArticleCategoryRepository>();
            serviceCollection.AddDbContext<BlogContext>(x => x.UseSqlServer(connectionString));

        }
    }
}

