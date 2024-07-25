using AccountManagement.Infrastructure.Configurations;
using BlogManagement.Infrastructure.Configurations;
using DiscountManagement.Infrastructure.Configurations;
using Framework.Application;
using Framework.Infrastructure;
using InventoryManagement.Infrastructure.Configurations;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ServiceHost.Services;
using ShopManagement.Infrastructure.Configuration;
using System.Collections.Generic;

namespace ServiceHost
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            ConnectionString = Configuration.GetConnectionString("LampshadeDb");
        }
        private string ConnectionString { get; set; }
        private IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddSingleton<IPasswordHasher, PasswordHasher>();
            services.AddTransient<IFileUploader, FileUploader>();
            services.AddTransient<IAuthHelper, AuthHelper>();
            ShopManagementBootstrapper.Configure(services, ConnectionString);
            DiscountManagementBootstrapper.Configure(services, ConnectionString);
            InventoryManagementBootstrapper.Configure(services, ConnectionString);
            AccountManagementBootstrapper.Configure(services, ConnectionString);
            BlogManagementBootstrapper.Configure(services, ConnectionString);
            services.Configure<CookiePolicyOptions>(opt =>
            {
                opt.CheckConsentNeeded = context => true;
                opt.MinimumSameSitePolicy = SameSiteMode.Strict;
            });
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(o =>
            {
                o.LogoutPath = new PathString("/Account");
                o.LoginPath = "/Index";
                o.AccessDeniedPath = "/AccessDenied";
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("AccountPolicy", policy => policy.RequireRole(new List<string> { RoleDefinitionHelper.Admin.Id.ToString() }));
                options.AddPolicy("BlogPolicy", policy => policy.RequireRole(new List<string> { RoleDefinitionHelper.Admin.Id.ToString(), RoleDefinitionHelper.ContentWriter.Id.ToString() }));
                options.AddPolicy("AdminAreaPolicy", policy => policy.RequireRole(new List<string> { RoleDefinitionHelper.Admin.Id.ToString(), RoleDefinitionHelper.ContentWriter.Id.ToString(), RoleDefinitionHelper.Salesman.Id.ToString(), RoleDefinitionHelper.WarehouseOperator.Id.ToString() }));

                options.AddPolicy("DiscountPolicy", policy => policy.RequireRole(new List<string> { RoleDefinitionHelper.Admin.Id.ToString(), RoleDefinitionHelper.Salesman.Id.ToString() }));

                options.AddPolicy("ShopPolicy", policy => policy.RequireRole(new List<string> { RoleDefinitionHelper.Admin.Id.ToString(), RoleDefinitionHelper.ContentWriter.Id.ToString() }));

                options.AddPolicy("WarehousePolicy", policy => policy.RequireRole(new List<string> { RoleDefinitionHelper.Admin.Id.ToString(), RoleDefinitionHelper.WarehouseOperator.Id.ToString() }));
            });

            services.AddRazorPages().AddRazorPagesOptions(builder =>
            {
                builder.Conventions.AuthorizeAreaFolder("Administration", "/", "AdminAreaPolicy");
                builder.Conventions.AuthorizeAreaFolder("Administration", "/Shop", "ShopPolicy");
                builder.Conventions.AuthorizeAreaFolder("Administration", "/Inventory", "WarehousePolicy");
                builder.Conventions.AuthorizeAreaFolder("Administration", "/Discounts", "DiscountPolicy");
                builder.Conventions.AuthorizeAreaFolder("Administration", "/Accounts", "AccountPolicy");

            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
