using AccountManagement.Application.Contracts.Role;
using Framework.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace ServiceHost.Areas.Administration.Pages.Roles
{
    public class IndexModel : PageModel
    {
        private readonly IRoleApplication _roleApplication;
        public List<RoleViewModel> Items { get; set; }
        public string SearchRoleName { get; set; }
        public IndexModel(IRoleApplication roleApplication)
        {
            _roleApplication = roleApplication;
        }

        public void OnGet(string searchRoleName)
        {
            SearchRoleName = searchRoleName;
            Items = _roleApplication.Search(SearchRoleName);

        }
        public PartialViewResult OnGetCreate()
        {
            return Partial("./Create", new CreateRole());
        }
        public PartialViewResult OnGetEdit(long id)
        {
            return Partial("./Edit", _roleApplication.EditGet(id));
        }
        public JsonResult OnPostCreate(CreateRole command)
        {
            if (ModelState.IsValid)
                return new JsonResult(_roleApplication.Create(command));
            return new JsonResult(ValidationMessages.InvalidModelStateMessage);
        }
        public JsonResult OnPostEdit(EditRole command)
        {
            if (ModelState.IsValid)
                return new JsonResult(_roleApplication.Edit(command));
            return new JsonResult(ValidationMessages.InvalidModelStateMessage);
        }
    }
}
