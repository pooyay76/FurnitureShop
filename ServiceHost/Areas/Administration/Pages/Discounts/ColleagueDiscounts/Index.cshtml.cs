using DiscountManagement.Application.Contracts.ColleagueDiscount;
using Framework.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Product;
using System.Collections.Generic;

namespace ServiceHost.Areas.Administration.Pages.Discounts.ColleagueDiscounts
{
    public class IndexModel : PageModel
    {
        private readonly IProductApplication productApplication;
        private readonly IColleagueDiscountApplication colleagueDiscountApplication;
        public ColleagueDiscountSearchModel SearchModel { get; set; }
        public List<ColleagueDiscountViewModel> Items { get; set; }
        public SelectList Products { get; set; }
        public IndexModel(IColleagueDiscountApplication colleagueDiscountApplication, IProductApplication productApplication)
        {
            this.colleagueDiscountApplication = colleagueDiscountApplication;
            this.productApplication = productApplication;
        }

        public void OnGet(ColleagueDiscountSearchModel command)
        {
            SearchModel = command;
            Items = colleagueDiscountApplication.Search(SearchModel);
        }
        public PartialViewResult OnGetCreate()
        {
            var form = new DefineColleagueDiscount() { Products = new SelectList(productApplication.List(), "Id", "Name") };
            return Partial("./Create", form);
        }
        public PartialViewResult OnGetEdit(long id)
        {
            var item = colleagueDiscountApplication.EditGet(id);
            item.Products = new SelectList(productApplication.List(), "Id", "Name");
            return Partial("./Edit", item);

        }
        public JsonResult OnPostCreate(DefineColleagueDiscount form)
        {
            if (ModelState.IsValid)
                return new JsonResult(colleagueDiscountApplication.Define(form));
            OperationResult operation = new();
            return new JsonResult(operation.Failed(ValidationMessages.InvalidModelStateMessage));
        }
        public JsonResult OnPostEdit(EditColleagueDiscount form)
        {
            if (ModelState.IsValid)
                return new JsonResult(colleagueDiscountApplication.Edit(form));
            OperationResult operation = new();
            return new JsonResult(operation.Failed(ValidationMessages.InvalidModelStateMessage));
        }
        public IActionResult OnGetDelete(long id)
        {
            var operation = new OperationResult();
            var item = colleagueDiscountApplication.Exists(id);
            if (item)
                return Partial("Delete", id);
            return new JsonResult(operation.Failed(ApplicationMessages.NotFoundMessage));

        }
        public IActionResult OnPostDelete(long id)
        {
            return new JsonResult(colleagueDiscountApplication.Delete(id));
        }
    }
}
