namespace AccountManagement.Application.Contracts.Account
{
    public class AccountSearchModel
    {
        public string Name { get; set; }
        public string Username { get; set; }
        public long RoleId { get; set; }
        public string PhoneNumber { get; set; }
    }
}
