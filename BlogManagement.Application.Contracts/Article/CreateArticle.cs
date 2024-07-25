using Framework.Application;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace BlogManagement.Application.Contracts.Article
{
    public class CreateArticle
    {
        [Required(ErrorMessage = ValidationMessages.RequiredMessage)]
        public string Title { get;  set; }
        [Required(ErrorMessage = ValidationMessages.RequiredMessage)]
        public string ShortDescription { get;  set; }
        [Required(ErrorMessage = ValidationMessages.RequiredMessage)]
        public string Description { get;  set; }
        [MaxFileSize(12, ErrorMessage = ValidationMessages.MaxFileSizeMessage)]
        [AllowedFileExtensions(new string[] { ".jpg", ".jpeg", ".png" })]
        [Required(ErrorMessage = ValidationMessages.RequiredMessage)]
        public IFormFile Picture { get;  set; }
        public string PictureAlt { get;  set; }
        public string PictureTitle { get;  set; }
        [Required(ErrorMessage = ValidationMessages.RequiredMessage)]
        public string Slug { get;  set; }
        [Required(ErrorMessage = ValidationMessages.RequiredMessage)]
        public string CanonicalAddress { get;  set; }
        public string Keywords { get;  set; }
        public long ArticleCategoryId { get;  set; }
        public long AuthorId { get;  set; }
    }
}
