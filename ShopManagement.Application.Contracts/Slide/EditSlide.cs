
using Framework.Application;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ShopManagement.Application.Contracts.Slide
{
    public class EditSlide
    {
        public long Id { get; set; }

        [MaxFileSize(12, ErrorMessage = ValidationMessages.MaxFileSizeMessage)]
        [AllowedFileExtensions(new string[] { ".jpg", ".jpeg", ".png" }, ErrorMessage = ValidationMessages.FileExtensionNotAllowed)]
        public IFormFile Picture { get; set; }
        public string PictureTitle { get; set; }
        public string PictureAlt { get; set; }
        public string Heading { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string BtnText { get; set; }
        [Required(ErrorMessage = ValidationMessages.RequiredMessage)]
        public string Link { get; set; }
    }
}
