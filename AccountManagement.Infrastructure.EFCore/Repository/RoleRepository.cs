using AccountManagement.Domain.RoleAgg;
using Framework.Infrastructure;
using System.Collections.Generic;
using System.Linq;

namespace AccountManagement.Infrastructure.EfCore.Repository
{
    public class RoleRepository : RepositoryBase<long, Domain.RoleAgg.Role>, IRoleRepository
    {
        private readonly AccountContext _context;

        public RoleRepository(AccountContext context) : base(context)
        {
            _context = context;

        }

        public IEnumerable<Domain.RoleAgg.Role> Search(string name)
        {
            var result = _context.Roles;
            if (string.IsNullOrWhiteSpace(name) == false)
            {
                result.Where(x => x.Name == name);
            }
            return result;
        }
    }
}
