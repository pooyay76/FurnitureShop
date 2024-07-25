using Framework.Domain;
using ShopManagement.Domain.ProductCategoryAgg;
using ShopManagement.Domain.ProductPictureAgg;
using System.Collections.Generic;

namespace ShopManagement.Domain.ProductAgg
{
    public class Product : EntityBase
    {
        public string Name { get; private set; }


        public string Code { get; private set; }

        public string Description { get; private set; }


        public string Picture { get; private set; }

        //Product Category

        public long CategoryId { get; private set; }
        public ProductCategory Category { get; private set; }

        //Product Pictures
        public List<ProductPicture> ProductPictures { get; private set; }


        //SEO
        public string Slug { get; private set; }

        public string ShortDescription { get; private set; }
        public string PictureAlt { get; private set; }
        public string PictureTitle { get; private set; }
        public string Keywords { get; private set; }
        public string MetaDescription { get; private set; }


        public Product(string name,string description,
            string picture, string pictureAlt, string pictureTitle, string keywords, string metaDescription,
            string slug, long categoryId, string code, string shortDescription)
        {
            Name = name;
            Description = description;
            Picture = picture;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            Keywords = keywords;
            MetaDescription = metaDescription;
            Slug = slug;
            CategoryId = categoryId;
            ShortDescription = shortDescription;
            Code = code;

        }
        public void Edit(string name, string description,
            string picture, string pictureAlt, string pictureTitle, string keywords, string metaDescription,
            string slug, long categoryId,string code,string shortDescription)
        {
            if (string.IsNullOrWhiteSpace(picture) == false)
            {
                Picture = picture;
            }

            Name = name;
            Description = description;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            Keywords = keywords;
            MetaDescription = metaDescription;
            Slug = slug;
            CategoryId = categoryId;
            ShortDescription = shortDescription;
            Code = code;
        }

    }


}
