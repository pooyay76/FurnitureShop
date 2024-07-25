using Framework.Application;
using System.ComponentModel.DataAnnotations;

namespace AccountManagement.Application.Contracts.Account
{
    public class Login
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = ValidationMessages.RequiredMessage)]
        public string Username { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = ValidationMessages.RequiredMessage)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
