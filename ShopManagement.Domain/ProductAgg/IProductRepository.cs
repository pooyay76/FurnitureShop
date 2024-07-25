using Framework.Domain;
using ShopManagement.Application.Contracts.Product;
using System.Collections.Generic;

namespace ShopManagement.Domain.ProductAgg
{
    public interface IProductRepository:IRepository<long,Product>
    {

        public Product GetProduct(long id);
        public Product GetProductWithCategory(long id);
        public Product GetProductWithPictures(long id);
        public Product GetProductWithCategoryAndPictures(long id);

        public IEnumerable<Product> GetProducts();
        public IEnumerable<Product> GetProductsWithCategories();
        public IEnumerable<Product> GetProductsWithPictures();
        public IEnumerable<Product> GetProductsWithCategoriesAndPictures();

        public IEnumerable<Product> Search(ProductSearchModel query);
    


    }
}
