using System.Linq;
using ShopManagement.Domain.ProductCategoryAgg;
using Framework.Infrastructure;
using System.Collections.Generic;
using ShopManagement.Application.Contracts.ProductCategory;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Domain.ProductAgg;

namespace ShopManagement.Infrastructure.EfCore.Repository
{
    public class ProductCategoryRepository : RepositoryBase<long, ProductCategory>, IProductCategoryRepository
    {
        private readonly ShopContext shopContext;

        public ProductCategoryRepository(ShopContext shopContext) : base(shopContext)
        {
            this.shopContext = shopContext;
        }

        public ProductCategory GetProductCategory(long id)
        {
            return shopContext.ProductCategories.FirstOrDefault(x => x.Id == id);

        }

        public ProductCategory GetProductCategoryWithProduct(long id)
        {
            return shopContext.ProductCategories.Include(x => x.Products).FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<ProductCategory> GetProductCategoriesWithProducts()
        {
            return shopContext.ProductCategories.Include(x => x.Products).OrderByDescending(x => x.Id);

        }

        public IEnumerable<Product> GetProducts(long id)
        {
            var target = shopContext.ProductCategories.Include(x => x.Products).FirstOrDefault(x => x.Id == id);
            return target?.Products.OrderByDescending(x => x.Id);
        }


        public IEnumerable<ProductCategory> GetProductCategories()
        {
            return shopContext.ProductCategories.OrderByDescending(x => x.Id);
        } 


        public IEnumerable<ProductCategory> Search(SearchProductCategoy searchQuery)
        {
            IQueryable<ProductCategory> query = shopContext.ProductCategories;
            if (searchQuery.Name != null)
                    query = query.Where(x => x.Name.Contains(searchQuery.Name));
            return query.OrderByDescending(x => x.Id);
        }


    } 
}
