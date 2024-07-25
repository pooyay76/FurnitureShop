using Framework.Domain;
using ShopManagement.Application.Contracts.ProductCategory;
using ShopManagement.Domain.ProductAgg;
using System.Collections.Generic;

namespace ShopManagement.Domain.ProductCategoryAgg
{
    public interface IProductCategoryRepository : IRepository<long,ProductCategory>
    {
        public ProductCategory GetProductCategory(long id);
        public IEnumerable<ProductCategory> GetProductCategories();
        public IEnumerable<ProductCategory> Search(SearchProductCategoy searchModel);
        public ProductCategory GetProductCategoryWithProduct(long id);
        public IEnumerable<ProductCategory> GetProductCategoriesWithProducts();
        public IEnumerable<Product> GetProducts(long id);



    }
}
