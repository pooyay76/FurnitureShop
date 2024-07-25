using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopManagement.Domain.ProductPictureAgg;

namespace ShopManagement.Infrastructure.EfCore.Mapping
{
    public class ProductPictureMapping : IEntityTypeConfiguration<ProductPicture>
    {
        public void Configure(EntityTypeBuilder<ProductPicture> builder)
        {
            builder.ToTable("ProductPictures");
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Product).WithMany(x => x.ProductPictures).HasForeignKey(x => x.ProductId);

            builder.Property(x => x.Picture).IsRequired().HasMaxLength(1000);
            builder.Property(x => x.ProductId).IsRequired().HasMaxLength(255);
            builder.Property(x => x.PictureTitle).HasMaxLength(255);
            builder.Property(x => x.PictureAlt).HasMaxLength(255);
        }
    }
}
