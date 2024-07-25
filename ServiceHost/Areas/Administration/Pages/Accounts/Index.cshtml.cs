using AccountManagement.Application.Contracts.Account;
using AccountManagement.Application.Contracts.Role;
using Framework.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace ServiceHost.Areas.Administration.Pages.Accounts
{
    public class IndexModel : PageModel
    {
        private readonly IAccountApplication accountApplication;
        private readonly IRoleApplication roleApplication;
        public AccountSearchModel SearchModel { get; set; }
        public List<AccountViewModel> Items { get; set; }
        public List<RoleViewModel> Roles { get; set; }

        public IndexModel(IAccountApplication accountApplication, IRoleApplication roleApplication)
        {
            this.accountApplication = accountApplication;
            this.roleApplication = roleApplication;
        }

        public void OnGet(AccountSearchModel searchModel)
        {
            SearchModel = searchModel;
            Items = accountApplication.Search(SearchModel);
            Roles = roleApplication.Search("");
        }
        public PartialViewResult OnGetCreate()
        {
            var result = new CreateAccount
            {
                Roles = roleApplication.Search("")
            };
            return Partial("./Create", result);
        }
        public PartialViewResult OnGetEdit(long id)
        {
            var result = accountApplication.EditGet(id);
            result.Roles = roleApplication.Search("");
            return Partial("./Edit", result);
        }
        public PartialViewResult OnGetChangePassword(long id)
        {
            return Partial("./ChangePassword", new ChangePassword() { Id = id });
        }
        public JsonResult OnPostCreate(CreateAccount command)
        {
            if (ModelState.IsValid)
                return new JsonResult(accountApplication.Create(command));
            return new JsonResult(ValidationMessages.InvalidModelStateMessage);
        }
        public JsonResult OnPostEdit(EditAccount command)
        {
            if (ModelState.IsValid)
                return new JsonResult(accountApplication.Edit(command));
            return new JsonResult(ValidationMessages.InvalidModelStateMessage);
        }
        public JsonResult OnPostChangePassword(ChangePassword command)
        {
            if (ModelState.IsValid)
                return new JsonResult(accountApplication.ChangePassword(command));
            return new JsonResult(ValidationMessages.InvalidModelStateMessage);
        }
    }
}
