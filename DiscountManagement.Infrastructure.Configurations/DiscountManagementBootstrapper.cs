using DiscountManagement.Application;
using DiscountManagement.Application.AutoMapperProfiles;
using DiscountManagement.Application.Contracts.ColleagueDiscount;
using DiscountManagement.Application.Contracts.CustomerDiscount;
using DiscountManagement.Domain.ColleagueDiscountAgg;
using DiscountManagement.Domain.CustomerDiscountAgg;
using DiscountManagement.Infrastructure.EfCore;
using DiscountManagement.Infrastructure.EfCore.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DiscountManagement.Infrastructure.Configurations
{
    public static class DiscountManagementBootstrapper 
    {
        public static void Configure(IServiceCollection services,string connectionString)
        {

            services.AddTransient<ICustomerDiscountApplication, CustomerDiscountApplication>();
            services.AddTransient<ICustomerDiscountRepository, CustomerDiscountRepository>();

            services.AddTransient<IColleagueDiscountRepository, ColleagueDiscountRepository>();
            services.AddTransient<IColleagueDiscountApplication, ColleagueDiscountApplication>();

            services.AddAutoMapper(typeof(ColleagueDiscountProfile).Assembly);

            services.AddDbContext<DiscountContext>(x=>x.UseSqlServer(connectionString));
        }
    }
}
