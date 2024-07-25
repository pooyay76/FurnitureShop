namespace AccountManagement.Domain.RoleAgg
{
    public class Permission
    {
        public long Id { get;private set; }
        public string Code { get;private set; }
        public string Name { get;private set; }
        public long RoleId { get;private set; }
        public Role Role { get;private set; }
        public Permission(string code, string name)
        {
            Code = code;
            Name = name;
        }

        public Permission(string code)
        {
            Code = code;
        }
    }
}
