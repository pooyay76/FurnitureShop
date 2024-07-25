using AccountManagement.Application.Contracts.Account;
using Framework.Domain;
using System.Collections.Generic;

namespace AccountManagement.Domain.AccountAgg
{
    public interface IAccountRepository:IRepository<long,Account>
    {
        IEnumerable<Account> Search(AccountSearchModel command);
        Account GetAccountByUsername(string username);
    }
}
