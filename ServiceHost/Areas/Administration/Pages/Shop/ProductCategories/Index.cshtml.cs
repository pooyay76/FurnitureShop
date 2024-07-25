using Framework.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contracts.ProductCategory;
using System.Collections.Generic;

namespace ServiceHost.Areas.Administration.Pages.Shop.ProductCategories
{
    public class IndexModel : PageModel
    {
        private readonly IProductCategoryApplication productCategoryApplication;
        public List<ProductCategoryMinimalViewModel> Items { get; set; }
        public SearchProductCategoy SearchModel { get; set; }


        public IndexModel(IProductCategoryApplication productCategoryApplication)
        {

            this.productCategoryApplication = productCategoryApplication;
        }


        public void OnGet(SearchProductCategoy command)
        {
            SearchModel = command;
            Items = productCategoryApplication.Search(SearchModel);
        }
        public PartialViewResult OnGetCreate()
        {
            return Partial("./Create", new CreateProductCategory());
        }
        public PartialViewResult OnGetEdit(long id)
        {
            var item = productCategoryApplication.EditGet(id);
            if (item != null)
                return Partial("./Edit", item);
            return null;

        }
        public JsonResult OnPostCreate(CreateProductCategory form)
        {
            if (ModelState.IsValid)
                return new JsonResult(productCategoryApplication.Create(form));
            else
            {
                OperationResult operation = new();
                return new JsonResult(operation.Failed(ValidationMessages.InvalidModelStateMessage));
            }
        }
        public JsonResult OnPostEdit(EditProductCategory form)
        {
            if (ModelState.IsValid)
            {
                return new JsonResult(productCategoryApplication.Edit(form));
            }
            else
            {
                OperationResult operation = new();
                return new JsonResult(operation.Failed(ValidationMessages.InvalidModelStateMessage));
            }
        }
        public IActionResult OnGetDelete(long id)
        {
            var operation = new OperationResult();
            var item = productCategoryApplication.Get(id);
            if (item != null)
                return Partial("Delete", id);
            return new JsonResult(operation.Failed(ApplicationMessages.NotFoundMessage));

        }
        public IActionResult OnPostDelete(long id)
        {
            return new JsonResult(productCategoryApplication.Delete(id));
        }
    }
}
