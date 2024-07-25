using System.Collections.Generic;

namespace LampshadeQuery.Contracts.ProductCategory
{
    public interface IProductCategoryQuery
    {
        public List<ProductCategoryQueryModel> GetProductCategories();
        public ProductCategoryQueryModel GetProductCategoryBySlug(string slug);
        public ProductCategoryQueryModel GetProductCategoryById(long id);
        public ProductCategoryQueryModel GetProductCategoryWithProducts(long id);
        public List<ProductCategoryQueryModel> GetProductCategoriesWithProductsDetails();

    }
}
