using AccountManagement.Domain.AccountAgg;
using AccountManagement.Domain.RoleAgg;
using AccountManagement.Infrastructure.EfCore.Mapping;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;

namespace AccountManagement.Infrastructure.EfCore
{
    public class AccountContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Role> Roles { get; set; }

        public AccountContext(DbContextOptions<AccountContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var assembly = typeof(AccountMapping).Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);


            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Role>().HasData(new { Name = RoleDefinitionHelper.Admin.Name, Id = RoleDefinitionHelper.Admin.Id, CreationDateTime = DateTime.Now });
            modelBuilder.Entity<Role>().HasData(new { Name = RoleDefinitionHelper.Salesman.Name, Id = RoleDefinitionHelper.Salesman.Id, CreationDateTime = DateTime.Now });
            modelBuilder.Entity<Role>().HasData(new { Name = RoleDefinitionHelper.WarehouseOperator.Name, Id = RoleDefinitionHelper.WarehouseOperator.Id, CreationDateTime = DateTime.Now });
            modelBuilder.Entity<Role>().HasData(new { Name = RoleDefinitionHelper.ContentWriter.Name, Id = RoleDefinitionHelper.ContentWriter.Id, CreationDateTime = DateTime.Now });
            modelBuilder.Entity<Role>().HasData(new { Name = RoleDefinitionHelper.NormalUser.Name, Id = RoleDefinitionHelper.NormalUser.Id, CreationDateTime = DateTime.Now });
        }
    }
}
