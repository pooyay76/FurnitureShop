using System.Collections.Generic;

namespace LampshadeQuery.Contracts.Product
{
    public interface IProductQuery
    {
        public List<ProductQueryModel> GetLatestArrivals(int number);
        public ProductQueryModel GetProductBySlug(string slug);
    }
}
