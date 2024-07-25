using AccountManagement.Application.Contracts.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class AccountModel : PageModel
    {
        private readonly IAccountApplication accountApplication;

        public RegisterAccount RegisterAccount { get; set; }
        public Login Login { get; set; }


        [TempData]
        public string LoginMessage { get; set; }
        [TempData]
        public string RegisterMessage { get; set; }
        public AccountModel(IAccountApplication accountApplication)
        {
            this.accountApplication = accountApplication;
        }

        public void OnGet()
        {
        }
        public IActionResult OnPostLogin(Login login)
        {
            Login = login;
            if (ModelState.IsValid)
            {
                var result = accountApplication.Login(Login);
                LoginMessage = result.Message;
                if (result.IsSucceeded)
                    return RedirectToPage("/Index");
                else
                    return Page();
            }

            return Page();
        }
        public IActionResult OnPostRegister(RegisterAccount registerAccount)
        {
            RegisterAccount = registerAccount;
            if (ModelState.IsValid)
            {
                var result = accountApplication.Register(RegisterAccount);
                RegisterMessage = result.Message;
                if (result.IsSucceeded)
                    return RedirectToPage("/Index");
            }
            return Page();
        }

        public IActionResult OnGetLogout()
        {
            accountApplication.Logout();
            return RedirectToPage("/Index");
        }
    }
}
