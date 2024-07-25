using Framework.Infrastructure;

namespace Framework.Application
{
    public class AuthViewModel
    {
        public string UserName { get; set; }
        public long AccountId { get; set; }
        public string FullName { get; set; }
        public long RoleId { get; set; }
        public RoleDefinition Role
        {
            get
            {
                return RoleDefinitionHelper.GetRoleById(RoleId);
            }
        }

        public AuthViewModel(long accountId, long roleId, string userName, string fullName)
        {
            UserName = userName;
            AccountId = accountId;
            RoleId = roleId;
            FullName = fullName;
        }
    }

}
