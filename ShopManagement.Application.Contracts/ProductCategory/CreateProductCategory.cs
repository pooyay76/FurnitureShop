using Framework.Application;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ShopManagement.Application.Contracts.ProductCategory
{
    public class CreateProductCategory
    {
        [Required(ErrorMessage = ValidationMessages.RequiredMessage)]
        public string Name { get; set; }

        [Required(ErrorMessage = ValidationMessages.RequiredMessage)]
        public string Description { get; set; }

        [Required(ErrorMessage = ValidationMessages.RequiredMessage)]
        [MaxFileSize(12, ErrorMessage = ValidationMessages.MaxFileSizeMessage)]
        [AllowedFileExtensions(new string[]{".jpg",".jpeg",".png"},ErrorMessage = ValidationMessages.FileExtensionNotAllowed)]
        public IFormFile Picture { get; set; }

        public string PictureAlt { get; set; }
        public string PictureTitle { get; set; }
        public string Keywords { get; set; }
        public string MetaDescription { get; set; }
        public string Slug { get; set; }
    }
}
