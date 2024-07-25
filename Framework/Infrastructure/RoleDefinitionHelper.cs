

namespace Framework.Infrastructure
{
    public static class RoleDefinitionHelper
    {
        public static RoleDefinition Admin { get; } = new(-1, "ادمین");
        public static RoleDefinition Salesman { get; } = new(-2, "مسؤل فروش");
        public static RoleDefinition WarehouseOperator { get; } = new(-3, "مسؤل انبار");
        public static RoleDefinition ContentWriter { get; } = new(-4, "محتوا گذار");
        public static RoleDefinition NormalUser { get; } = new(-5, "کاربر معمولی");

        public static RoleDefinition Anonymous { get; } = new(-6, "کاربر بی هویت");

        public static RoleDefinition GetRoleById(long id)
        {
            switch (id)
            {
                case -1:
                    return Admin;
                case -2:
                    return Salesman;
                case -3:
                    return WarehouseOperator;
                case -4:
                    return ContentWriter;
                case -5:
                    return NormalUser;
                default:
                    return NormalUser;
            }
        }
    }
    public struct RoleDefinition
    {
        public long Id { get; private set; }
        public string Name { get; private set; }

        public RoleDefinition(long id, string name)
        {
            Id = id;
            Name = name;
        }
    }

}
