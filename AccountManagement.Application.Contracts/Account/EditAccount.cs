using AccountManagement.Application.Contracts.Role;
using Framework.Application;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AccountManagement.Application.Contracts.Account
{
    public class EditAccount
    {
        public long Id { get; set; }
        [Required(ErrorMessage = ValidationMessages.RequiredMessage)]
        public string FullName { get; set; }
        [Required(ErrorMessage = ValidationMessages.RequiredMessage)]
        public string Username { get; set; }
        [Required(ErrorMessage = ValidationMessages.RequiredMessage)]
        public long RoleId { get; set; }
        [Phone(ErrorMessage = ValidationMessages.PhoneNumberIncorrect)]
        [Required(ErrorMessage = ValidationMessages.RequiredMessage)]
        public string PhoneNumber { get; set; }
        [MaxFileSize(12, ErrorMessage = ValidationMessages.MaxFileSizeMessage)]
        [AllowedFileExtensions(new string[] { ".jpg", ".jpeg", ".png" }, ErrorMessage = ValidationMessages.FileExtensionNotAllowed)]
        public IFormFile ProfilePicture { get; set; }
        public List<RoleViewModel> Roles { get; set; }
    }
}
