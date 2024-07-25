using AccountManagement.Application.Contracts.Account;
using AccountManagement.Domain.AccountAgg;
using Framework.Infrastructure;
using System.Collections.Generic;
using System.Linq;

namespace AccountManagement.Infrastructure.EfCore.Repository
{
    public class AccountRepository : RepositoryBase<long, Account>, IAccountRepository
    {
        private readonly AccountContext accountContext;
        public AccountRepository(AccountContext context) : base(context)
        {

            accountContext = context;
        }

        public Account GetAccountByUsername(string username)
        {
            return accountContext.Accounts.FirstOrDefault(x => x.Username == username);
        }


        public IEnumerable<Account> Search(AccountSearchModel command)
        {
            var query = accountContext.Accounts.AsQueryable();
            if (!string.IsNullOrWhiteSpace(command.Username))
                query = query.Where(x => x.Username.Contains(command.Username));

            if (!string.IsNullOrWhiteSpace(command.Name))
                query = query.Where(x => x.FullName.Contains(command.Name));

            if (!string.IsNullOrWhiteSpace(command.PhoneNumber))
                query = query.Where(x => x.PhoneNumber.Contains(command.PhoneNumber));

            if (command.RoleId != default)
                query = query.Where(x => x.RoleId == command.RoleId);

            if (query == null)
                return null;

            return query.OrderByDescending(x => x.Id);

        }
    }
}
