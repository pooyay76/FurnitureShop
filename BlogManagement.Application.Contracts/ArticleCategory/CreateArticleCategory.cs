using Framework.Application;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace BlogManagement.Application.Contracts.ArticleCategory
{
    public class CreateArticleCategory
    {
        [Required(ErrorMessage = ValidationMessages.RequiredMessage)]
        public string Name { get; set; }
        [MaxFileSize(12,ErrorMessage = ValidationMessages.MaxFileSizeMessage)]
        [AllowedFileExtensions(new string[] { ".jpg", ".jpeg", ".png" })]
        [Required(ErrorMessage = ValidationMessages.RequiredMessage)]
        public IFormFile Picture { get; set; }
        public string PictureAlt { get; set; }
        public string PictureTitle { get; set; }
        public string Description { get; set; }
        [Required(ErrorMessage = ValidationMessages.RequiredMessage)]
        public int ShowOrder { get; set; }
        [Required(ErrorMessage = ValidationMessages.RequiredMessage)]
        public string Slug { get; set; }
        public string Keywords { get; set; }
        public string MetaDescription { get; set; }
        public string CanonicalAddress { get; set; }
    }
}
