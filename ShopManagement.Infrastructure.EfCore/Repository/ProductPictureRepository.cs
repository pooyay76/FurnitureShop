using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contracts.ProductPicture;
using ShopManagement.Domain.ProductPictureAgg;
using System.Collections.Generic;
using System.Linq;

namespace ShopManagement.Infrastructure.EfCore.Repository
{
    public class ProductPictureRepository : RepositoryBase<long, ProductPicture>, IProductPictureRepository
    {
        private readonly ShopContext shopContext;

        public ProductPictureRepository(ShopContext shopContext) : base(shopContext)
        {
            this.shopContext = shopContext;
        }
        public ProductPicture GetProductPicture(long id)
        {
            return shopContext.ProductPictures.Include(x => x.Product).FirstOrDefault(x => x.Id == id);

        }
        public ProductPicture GetProductPictureWithProduct(long id)
        {
            return shopContext.ProductPictures.Include(x => x.Product).FirstOrDefault(x => x.Id == id);

        }


        public IEnumerable<ProductPicture> GetProductPictures()
        {
            return shopContext.ProductPictures.OrderByDescending(x => x.Id);
        }
        public IEnumerable<ProductPicture> GetProductPicturesWithProducts()
        {
            return shopContext.ProductPictures.Include(x => x.Product).OrderByDescending(x => x.Id);
        }
        public IEnumerable<ProductPicture> Search(SearchProductPicture query)
        {

            IQueryable<ProductPicture> queryable = shopContext.ProductPictures.Include(x => x.Product);
            if (query.ProductName != null)
                queryable = queryable.Where(x => x.Product.Name.Contains(query.ProductName));

            return queryable.OrderByDescending(x => x.Id);
        }


    }
}
