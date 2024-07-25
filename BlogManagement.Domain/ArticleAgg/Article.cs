using BlogManagement.Domain.ArticleCategoryAgg;
using Framework.Domain;
using System;

namespace BlogManagement.Domain.ArticleAgg
{
    public class Article:EntityBase
    {
        public string Title { get; private set; }
        public string ShortDescription { get; private set; }
        public string Description { get; private set; }
        public string Picture { get; private set; }
        public string PictureAlt { get; private set; }
        public string PictureTitle { get; private set; }
        public string Slug { get; private set; }
        public string CanonicalAddress { get; private set; }
        public string Keywords { get; private set; }
        public long ArticleCategoryId { get; private set; }
        public long AuthorId { get; private set; }
        public ArticleCategory ArticleCategory { get; private set; }

        public Article(string title, string shortDescription, string description, string picture, string pictureAlt, string pictureTitle, 
            string slug, string canonicalAddress, string keywords, long articleCategoryId, long authorId)
        {
            Title = title;
            ShortDescription = shortDescription;
            Description = description;
            Picture = picture;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            Slug = slug;
            CanonicalAddress = canonicalAddress;
            Keywords = keywords;
            ArticleCategoryId = articleCategoryId;
            AuthorId = authorId;
        }
        public void Edit(string title, string shortDescription, string description, string picture, string pictureAlt, string pictureTitle,
            string slug, string canonicalAddress, string keywords, long articleCategoryId)
        {
            Title = title;
            ShortDescription = shortDescription;
            Description = description;
            if(string.IsNullOrWhiteSpace(picture) == false)
                Picture = picture;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            Slug = slug;
            CanonicalAddress = canonicalAddress;
            Keywords = keywords;
            ArticleCategoryId = articleCategoryId;
        }
    }
}
