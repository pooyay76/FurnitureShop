using AccountManagement.Domain.RoleAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccountManagement.Infrastructure.EfCore.Mapping
{
    public class RoleMapping : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {

            builder.ToTable("Roles");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).HasMaxLength(128).IsRequired();
            builder.HasMany(x=>x.Accounts).WithOne(x => x.Role).HasForeignKey(x=>x.RoleId);

            builder.OwnsMany(x => x.Permissions, nav =>
            {
                nav.ToTable("Role Permissions");
                nav.HasKey(y => y.Id);
                nav.WithOwner(y => y.Role).HasForeignKey(y=>y.RoleId);
                
            });
        }
    }
}
