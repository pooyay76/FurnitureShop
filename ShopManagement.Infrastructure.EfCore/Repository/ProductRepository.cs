using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Domain.ProductAgg;
using System.Collections.Generic;
using System.Linq;

namespace ShopManagement.Infrastructure.EfCore.Repository
{
    public class ProductRepository :RepositoryBase<long, Product>, IProductRepository
    {
        private readonly ShopContext shopContext;
        public ProductRepository(ShopContext shopContext) : base(shopContext)
        {
            this.shopContext = shopContext;
        }

        public Product GetProduct(long id)
        {
            return shopContext.Products.FirstOrDefault(x => x.Id == id);
        }


        public Product GetProductWithCategory(long id)
        {
            return shopContext.Products.Include(x => x.Category).FirstOrDefault(x=>id == x.Id);

        }

        public Product GetProductWithPictures(long id)
        {
            return shopContext.Products.Include(x => x.ProductPictures).FirstOrDefault(x => x.Id == id);

        }
        public Product GetProductWithCategoryAndPictures(long id)
        {
            return shopContext.Products.Include(x => x.Category).Include(x=>x.ProductPictures).FirstOrDefault(x => id == x.Id);

        }


        public IEnumerable<Product> GetProducts()
        {
            return shopContext.Products.OrderByDescending(x => x.Id);
        }

        public IEnumerable<Product> GetProductsWithPictures()
        {
            return shopContext.Products.Include(x=>x.ProductPictures).OrderByDescending(x => x.Id);
        }

        public IEnumerable<Product> GetProductsWithCategories()
        {
            return shopContext.Products.Include(x => x.Category).OrderByDescending(x => x.Id);
        }
        public IEnumerable<Product> GetProductsWithCategoriesAndPictures()
        {
            return shopContext.Products.Include(x => x.Category).Include(x => x.ProductPictures).OrderByDescending(x => x.Id);
        }

        public IEnumerable<Product> Search(ProductSearchModel query)
        {
            var result = shopContext.Products.Include(x => x.Category).AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.Name))
                result = result.Where(x => x.Name.Contains(query.Name));

            if (query.CategoryId != 0)
                result = result.Where(x => x.Category.Id == query.CategoryId);

            if (query.Code != null)
                result = result.Where(x => x.Code == query.Code);


            return result.OrderByDescending(x => x.Id);

        }

    }
}
