namespace AccountManagement.Application.Contracts.Account
{
    public class AccountViewModel
    {
        public long Id { get; set; }
        public string FullName { get; set; }
        public string Username { get; set; }
        public long RoleId { get; set; }
        public string PhoneNumber { get; set; }
        public string ProfilePicture { get; set; }
    }
}
