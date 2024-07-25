using Framework.Domain;
using ShopManagement.Domain.ProductAgg;

namespace ShopManagement.Domain.ProductPictureAgg
{
    public class ProductPicture:EntityBase
    {
        public string Picture { get; private set; }
        public string PictureAlt { get; private set; }
        public string PictureTitle { get; private set; }
        public Product Product { get; private set; }
        public long ProductId { get; private set; }

        public ProductPicture(string picture, string pictureAlt, string pictureTitle, long productId)
        {
            Picture = picture;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            ProductId = productId;
        }
        public void Edit(string picture, string pictureAlt, string pictureTitle, long productId)
        {
            if (string.IsNullOrWhiteSpace(picture) == false) 
            {
            Picture = picture;
            }

            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            ProductId = productId;
        }
    }

}
