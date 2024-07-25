using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopManagement.Domain.ProductAgg;

namespace ShopManagement.Infrastructure.EfCore.Mapping
{
    public class ProductMapping : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Category).WithMany(x => x.Products).HasForeignKey(x => x.CategoryId);
            builder.HasMany(x => x.ProductPictures).WithOne(x => x.Product).HasForeignKey(x => x.ProductId);


            builder.Property(x => x.Name).HasMaxLength(80).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(510).IsRequired();
            builder.Property(x => x.ShortDescription).HasMaxLength(255);
            builder.Property(x => x.Picture).HasMaxLength(1000).IsRequired();
            builder.Property(x => x.CategoryId).IsRequired();
            builder.Property(x => x.Code).IsRequired();

            builder.Property(x => x.PictureAlt).HasMaxLength(255);
            builder.Property(x => x.Slug).HasMaxLength(300).IsRequired();
            builder.Property(x => x.PictureTitle).HasMaxLength(255);
            builder.Property(x => x.Keywords).HasMaxLength(80);
            builder.Property(x => x.MetaDescription).HasMaxLength(150);

        }
    }
}
