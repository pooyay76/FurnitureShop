using DiscountManagement.Application.Contracts.CustomerDiscount;
using Framework.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Product;
using System.Collections.Generic;

namespace ServiceHost.Areas.Administration.Pages.Discounts.CustomerDiscounts
{
    public class IndexModel : PageModel
    {
        private readonly IProductApplication productApplication;
        private readonly ICustomerDiscountApplication customerDiscountApplication;
        public CustomerDiscountSearchModel SearchModel { get; set; }
        public List<CustomerDiscountViewModel> Items { get; set; }
        public SelectList Products { get; set; }
        public IndexModel(ICustomerDiscountApplication customerDiscountApplication, IProductApplication productApplication)
        {
            this.customerDiscountApplication = customerDiscountApplication;
            this.productApplication = productApplication;
        }

        public void OnGet(CustomerDiscountSearchModel command)
        {
            SearchModel = command;
            Items = customerDiscountApplication.Search(SearchModel);
            Products = new SelectList(productApplication.List(), "Id", "Name");
        }
        public PartialViewResult OnGetCreate()
        {
            var form = new DefineCustomerDiscount() { Products = new SelectList(productApplication.List(), "Id", "Name") };
            return Partial("./Create", form);
        }
        public PartialViewResult OnGetEdit(long id)
        {
            var item = customerDiscountApplication.EditGet(id);

            item.Products = new SelectList(productApplication.List(), "Id", "Name");
            if (item == null)
                return null;
            return Partial("./Edit", item);

        }
        public JsonResult OnPostCreate(DefineCustomerDiscount form)
        {
            if (ModelState.IsValid)
                return new JsonResult(customerDiscountApplication.Define(form));
            OperationResult operation = new();
            return new JsonResult(operation.Failed(ValidationMessages.InvalidModelStateMessage));
        }
        public JsonResult OnPostEdit(EditCustomerDiscount form)
        {
            if (ModelState.IsValid)
                return new JsonResult(customerDiscountApplication.Edit(form));
            OperationResult operation = new();
            return new JsonResult(operation.Failed(ValidationMessages.InvalidModelStateMessage));
        }
        public IActionResult OnGetDelete(long id)
        {
            var operation = new OperationResult();
            bool exists = customerDiscountApplication.Exists(id);
            if (exists)
                return Partial("Delete", id);
            return new JsonResult(operation.Failed(ApplicationMessages.NotFoundMessage));

        }
        public IActionResult OnPostDelete(long id)
        {
            return new JsonResult(customerDiscountApplication.Delete(id));
        }
    }
}
