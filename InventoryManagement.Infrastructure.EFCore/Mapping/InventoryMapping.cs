
using InventoryManagement.Domain.InventoryAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryManagement.Infrastructure.EFCore.Mapping
{
    public class InventoryMapping : IEntityTypeConfiguration<Inventory>
    {
        public void Configure(EntityTypeBuilder<Inventory> builder)
        {
            builder.ToTable("Inventories");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.UnitPrice).HasColumnType("decimal(10,2)");
            builder.OwnsMany(x => x.InventoryOperations, builder2 =>
               {
                   builder2.HasKey(x => x.Id);
                   builder2.ToTable("Inventory Operations");
                   builder2.WithOwner(x => x.Inventory).HasForeignKey(x => x.InventoryId);
               });
        }
    }
}
