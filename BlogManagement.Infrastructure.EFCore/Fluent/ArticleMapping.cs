using BlogManagement.Domain.ArticleAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogManagement.Infrastructure.EFCore.Fluent
{
    public class ArticleMapping : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.HasKey(x=> x.Id);
            builder.Property(x=>x.Title).IsRequired().HasMaxLength(500);
            builder.Property(x=>x.ShortDescription).IsRequired().HasMaxLength(500);
            builder.Property(x=>x.Description).IsRequired().HasMaxLength(5000);
            builder.Property(x => x.CanonicalAddress).IsRequired().HasMaxLength(500);
            builder.Property(x => x.ArticleCategoryId).IsRequired();
            builder.Property(x => x.AuthorId).IsRequired();
            builder.HasOne(x=>x.ArticleCategory).WithMany(y=>y.Articles).HasForeignKey(x=>x.ArticleCategoryId).IsRequired().OnDelete(DeleteBehavior.Restrict);
        }
    }
}
