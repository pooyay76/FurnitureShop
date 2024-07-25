using Microsoft.Extensions.DependencyInjection;
using AccountManagement.Infrastructure.EfCore;
using Microsoft.EntityFrameworkCore;
using AccountManagement.Domain.AccountAgg;
using AccountManagement.Infrastructure.EfCore.Repository;
using AccountManagement.Application.Contracts.Account;
using AccountManagement.Application;
using AccountManagement.Application.AutoMapperProfiles;
using AccountManagement.Domain.RoleAgg;
using AccountManagement.Application.Contracts.Role;

namespace AccountManagement.Infrastructure.Configurations
{
    public static class AccountManagementBootstrapper
    {
        public static void Configure(IServiceCollection services, string connectionString)
        {
            services.AddAutoMapper(typeof(AccountProfile).Assembly);

            services.AddTransient<IRoleRepository, RoleRepository>();
            services.AddTransient<IRoleApplication, RoleApplication>();
            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddTransient<IAccountApplication, AccountApplication>();
            services.AddDbContext<AccountContext>(x => x.UseSqlServer(connectionString));
        }
    }
}
