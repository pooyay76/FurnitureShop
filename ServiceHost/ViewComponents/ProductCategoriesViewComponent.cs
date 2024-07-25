using LampshadeQuery.Contracts.ProductCategory;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.ViewComponents
{
    public class ProductCategoriesViewComponent:ViewComponent
    {
        private readonly IProductCategoryQuery productCategoryQuery;

        public ProductCategoriesViewComponent(IProductCategoryQuery productCategoryQuery)
        {
            this.productCategoryQuery = productCategoryQuery;
        }
        public IViewComponentResult Invoke()
        {
            return View(productCategoryQuery.GetProductCategories());
        }
    } 
}
