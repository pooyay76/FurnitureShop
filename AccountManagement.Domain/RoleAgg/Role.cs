using AccountManagement.Domain.AccountAgg;
using Framework.Domain;
using System.Collections.Generic;

namespace AccountManagement.Domain.RoleAgg
{



    public class Role:EntityBase
    {
        public string Name { get;private set; }
        public List<Account> Accounts{ get;private set; }
        public List<Permission> Permissions { get;private set; }
        public Role(string name, List<Permission> permissions)
        {
            Permissions = permissions;
            Name = name;
            Accounts = new List<Account>();
        }
        private Role()
        {

        }
        public void Edit(string name,List<Permission> permissions)
        {
            Name = name;
            Permissions = permissions;
        }
    }
}
