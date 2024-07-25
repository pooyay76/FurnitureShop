using AccountManagement.Domain.RoleAgg;
using Framework.Domain;

namespace AccountManagement.Domain.AccountAgg
{
    public class Account:EntityBase
    {
        public string FullName { get; private set; }
        public string Username { get; private set; }
        public string Password { get; private set; }
        public string ProfilePicture { get; private set; }
        public long RoleId { get; private set; }
        public Role Role { get; private set; }
        public string PhoneNumber { get; private set; }

        public Account(string fullName, string username, string password, string profilePicture, long roleId, string phoneNumber)
        {
            FullName = fullName;
            Username = username;
            Password = password;
            ProfilePicture = profilePicture;
            RoleId = roleId;
            PhoneNumber = phoneNumber;
        }

        public void Edit(string fullName, string username, string profilePicture, long roleId, string phoneNumber)
        {
            FullName = fullName;
            Username = username;
            if (string.IsNullOrWhiteSpace(profilePicture) == false)
                ProfilePicture = profilePicture;
            RoleId = roleId;
            PhoneNumber = phoneNumber;
        }
        public void ChangePassword(string password)
        {
            Password = password;
        }
    }
}
