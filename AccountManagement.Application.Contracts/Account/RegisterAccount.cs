using Framework.Application;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace AccountManagement.Application.Contracts.Account
{
    public class RegisterAccount
    {
        [Required(ErrorMessage = ValidationMessages.RequiredMessage)]
        [RegularExpression(@"^[آ-ی]+\s+[آ-ی]+$",
         ErrorMessage = ValidationMessages.NameIncorrect)]
        public string FullName { get; set; }

        [Required(ErrorMessage = ValidationMessages.RequiredMessage)]
        [MaxLength(20, ErrorMessage = ValidationMessages.MaximumLength20)]
        [RegularExpression(@"[a-zA-Z0-9]{4,20}",
         ErrorMessage = ValidationMessages.UsernameIncorrect)]
        public string Username { get; set; }

        [Required(ErrorMessage = ValidationMessages.RequiredMessage)]
        [MinLength(8, ErrorMessage = ValidationMessages.PasswordIncorrect)]
        [RegularExpression(@"[a-zA-Z]+[0-9]+|[0-9]+[a-zA-Z]+",
         ErrorMessage = ValidationMessages.PasswordIncorrect)]
        public string Password { get; set; }

        [Required(ErrorMessage = ValidationMessages.RequiredMessage)]
        [Compare("Password", ErrorMessage = ValidationMessages.PasswordsMismatchMessage)]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = ValidationMessages.RequiredMessage)]
        [RegularExpression(@"09[0-9]{9}",
         ErrorMessage = ValidationMessages.PhoneNumberIncorrect)]
        public string PhoneNumber { get; set; }

        [MaxFileSize(12, ErrorMessage = ValidationMessages.MaxFileSizeMessage)]
        [AllowedFileExtensions(new string[] { ".jpg", ".jpeg", ".png" }, ErrorMessage = ValidationMessages.FileExtensionNotAllowed)]
        public IFormFile ProfilePicture { get; set; }

    }
}
