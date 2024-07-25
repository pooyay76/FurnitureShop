using LampshadeQuery.Contracts.Product;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class ProductModel : PageModel
    {
        private readonly IProductQuery productQuery;
        public ProductQueryModel Product { get; set; }
        public ProductModel(IProductQuery productQuery)
        {
            this.productQuery = productQuery;
        }

        public void OnGet(string name)
        {
            Product = productQuery.GetProductBySlug(name);
        }
    }
}
