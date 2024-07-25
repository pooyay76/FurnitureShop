using BlogManagement.Domain.ArticleAgg;
using Framework.Domain;
using System;
using System.Collections.Generic;

namespace BlogManagement.Domain.ArticleCategoryAgg
{
    public class ArticleCategory:EntityBase
    {
        public string Name { get; private set; }
        public string Picture { get; private set; }
        public string PictureAlt { get; private set; }
        public string PictureTitle { get; private set; }
        public string Description { get; private set; }
        public int ShowOrder { get; private set; }
        public string Slug { get; private set; }
        public string Keywords { get; private set; }
        public string MetaDescription { get; private set; }
        public string CanonicalAddress { get; private set; }
        public List<Article> Articles{ get; set; }
        public ArticleCategory(string name, string picture, string description, int showOrder,
            string slug, string keywords, string metaDescription, string canonicalAddress, string pictureTitle, string pictureAlt)
        {
            Name = name;
            Picture = picture;
            Description = description;
            ShowOrder = showOrder;
            Slug = slug;
            Keywords = keywords;
            MetaDescription = metaDescription;
            CanonicalAddress = canonicalAddress;
            PictureTitle = pictureTitle;
            PictureAlt = pictureAlt;
        }
        public void Edit(string name, string picture, string description, int showOrder,
            string slug, string keywords, string metaDescription, string canonicalAddress, string pictureTitle, string pictureAlt)
        {
            Name = name;
            if (string.IsNullOrWhiteSpace(picture) == false)
            {
                Picture = picture;
            }
            Description = description;
            ShowOrder = showOrder;
            Slug = slug;
            Keywords = keywords;
            MetaDescription = metaDescription;
            CanonicalAddress = canonicalAddress; 
            PictureTitle = pictureTitle;
            PictureAlt = pictureAlt;
        }
    }
}
