using LampshadeQuery.Contracts.ProductCategory;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class ProductCategoryModel : PageModel
    {
        private readonly IProductCategoryQuery productCategoryQuery;
        public ProductCategoryQueryModel ProductCategory { get; set; }
        public ProductCategoryModel(IProductCategoryQuery productCategoryQuery)
        {
            this.productCategoryQuery = productCategoryQuery;
        }

        public void OnGet(string name)
        {
            ProductCategory = productCategoryQuery.GetProductCategoryBySlug(name);
        }
    }
}
