using BlogManagement.Domain.ArticleCategoryAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogManagement.Infrastructure.EFCore.Fluent
{
    public class ArticleCategoryMapping : IEntityTypeConfiguration<ArticleCategory>
    {
        public void Configure(EntityTypeBuilder<ArticleCategory> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Picture).IsRequired().HasMaxLength(128);
            builder.Property(x=>x.Description).IsRequired().HasMaxLength(128);
            builder.Property(x => x.ShowOrder).IsRequired();
            builder.Property(x => x.Name).IsRequired().HasMaxLength(128);
            builder.Property(x => x.Slug).IsRequired().HasMaxLength(128);
            builder.HasMany(x=>x.Articles).WithOne(y=>y.ArticleCategory).HasForeignKey(y=>y.ArticleCategoryId).IsRequired().OnDelete(DeleteBehavior.Restrict);
        }
    }
}
