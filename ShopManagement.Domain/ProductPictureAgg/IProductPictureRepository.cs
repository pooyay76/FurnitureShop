using Framework.Domain;
using ShopManagement.Application.Contracts.ProductPicture;
using System.Collections.Generic;

namespace ShopManagement.Domain.ProductPictureAgg
{
    public interface IProductPictureRepository : IRepository<long,ProductPicture>
    {
        public ProductPicture GetProductPicture(long id);
        public IEnumerable<ProductPicture> GetProductPictures();
        public IEnumerable<ProductPicture> Search(SearchProductPicture query);

        public ProductPicture GetProductPictureWithProduct(long id);
        public IEnumerable<ProductPicture> GetProductPicturesWithProducts();

    }
}
