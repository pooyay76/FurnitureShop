using Framework.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contracts.Slide;
using System.Collections.Generic;

namespace ServiceHost.Areas.Administration.Pages.Shop.Slides
{
    public class IndexModel : PageModel
    {
        private readonly ISlideApplication slideApplication;
        public List<SlideViewModel> Items { get; set; }
        public SearchSlide SearchModel { get; set; }

        public IndexModel(ISlideApplication slideApplication)
        {
            this.slideApplication = slideApplication;
        }

        public void OnGet(SearchSlide command)
        {
            SearchModel = command;
            Items = slideApplication.Search(SearchModel);
        }


        public PartialViewResult OnGetCreate()
        {
            return Partial("./Create", new CreateSlide());
        }

        public JsonResult OnPostCreate(CreateSlide form)
        {
            var operation = new OperationResult();

            if (ModelState.IsValid)
            {
                return new JsonResult(slideApplication.Create(form));
            }
            else
                return new JsonResult(operation.Failed(ValidationMessages.InvalidModelStateMessage));
        }
        public IActionResult OnGetEdit(long id)
        {
            var operation = new OperationResult();
            var item = slideApplication.EditGet(id);
            if (item != null)
                return Partial("./Edit", item);
            return new JsonResult(operation.Failed(ApplicationMessages.NotFoundMessage));

        }
        public JsonResult OnPostEdit(EditSlide form)
        {
            var operation = new OperationResult();
            if (ModelState.IsValid)
            {
                return new JsonResult(slideApplication.Edit(form));
            }
            return new JsonResult(operation.Failed(ValidationMessages.InvalidModelStateMessage));
        }
        public IActionResult OnGetDelete(long id)
        {
            var operation = new OperationResult();
            var item = slideApplication.Get(id);
            if (item != null)
                return Partial("Delete", id);
            return new JsonResult(operation.Failed(ApplicationMessages.NotFoundMessage));

        }
        public IActionResult OnPostDelete(long id)
        {
            return new JsonResult(slideApplication.Delete(id));
        }
    }

}
