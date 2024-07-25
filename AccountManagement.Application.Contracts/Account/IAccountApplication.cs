using Framework.Application;
using System.Collections.Generic;

namespace AccountManagement.Application.Contracts.Account
{
    public interface IAccountApplication
    {
        OperationResult Create(CreateAccount command);
        OperationResult Edit(EditAccount command);
        OperationResult ChangePassword(ChangePassword command);
        OperationResult Register(RegisterAccount command);

        EditAccount EditGet(long id);
        OperationResult Login(Login command);
        void Logout();
        List<AccountViewModel> Search(AccountSearchModel command);
    }

}
