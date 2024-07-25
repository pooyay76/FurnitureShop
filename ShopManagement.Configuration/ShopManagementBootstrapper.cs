using LampshadeQuery.Contracts.Product;
using LampshadeQuery.Contracts.ProductCategory;
using LampshadeQuery.Contracts.Slide;
using LampshadeQuery.Query;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShopManagement.Application;
using ShopManagement.Application.AutoMapperProfiles;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Application.Contracts.ProductCategory;
using ShopManagement.Application.Contracts.ProductPicture;
using ShopManagement.Application.Contracts.Slide;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductCategoryAgg;
using ShopManagement.Domain.ProductPictureAgg;
using ShopManagement.Domain.SlideAgg;
using ShopManagement.Infrastructure.EfCore;
using ShopManagement.Infrastructure.EfCore.Repository;

namespace ShopManagement.Infrastructure.Configuration
{
    public class ShopManagementBootstrapper
    {
        public static void Configure(IServiceCollection serviceCollection, string connectionString)
        {
            serviceCollection.AddAutoMapper(typeof(ProductCategoryProfile).Assembly);

            serviceCollection.AddTransient<IProductCategoryRepository, ProductCategoryRepository>();
            serviceCollection.AddTransient<IProductCategoryApplication, ProductCategoryApplication>();
            serviceCollection.AddTransient<IProductCategoryQuery, ProductCategoryQuery>();

            serviceCollection.AddTransient<IProductApplication, ProductApplication>();
            serviceCollection.AddTransient<IProductRepository, ProductRepository>();
            serviceCollection.AddTransient<IProductQuery, ProductQuery>();

            serviceCollection.AddTransient<IProductPictureRepository, ProductPictureRepository>();
            serviceCollection.AddTransient<IProductPictureApplication, ProductPictureApplication>();

            serviceCollection.AddTransient<ISlideApplication, SlideApplication>();
            serviceCollection.AddTransient<ISlideRepository, SlideRepository>();
            serviceCollection.AddTransient<ISlideQuery, SlidesQuery>();

            serviceCollection.AddDbContext<ShopContext>(x => x.UseSqlServer(connectionString));

        }
    }
}
