using Framework.Domain;
using System.Collections.Generic;

namespace AccountManagement.Domain.RoleAgg
{
    public interface IRoleRepository:IRepository<long,Role>
    {
        IEnumerable<Role> Search(string name);
    }
}
