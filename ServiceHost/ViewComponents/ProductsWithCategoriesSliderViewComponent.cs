using LampshadeQuery.Contracts.ProductCategory;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.ViewComponents
{
    public class ProductsWithCategoriesSliderViewComponent : ViewComponent
    {
        private readonly IProductCategoryQuery productCategoryQuery;

        public ProductsWithCategoriesSliderViewComponent(IProductCategoryQuery productCategoryQuery)
        {
            this.productCategoryQuery = productCategoryQuery;
        }

        public IViewComponentResult Invoke()
        {
            var items = productCategoryQuery.GetProductCategoriesWithProductsDetails();
            return View(items);
        }
    }
}
