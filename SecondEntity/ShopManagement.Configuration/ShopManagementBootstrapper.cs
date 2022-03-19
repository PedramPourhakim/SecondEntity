using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShopManagement.Application;
using ShopManagement.Application.Contracts.ProductCategoryContracts;
using ShopManagement.Domain.ProductCategoryAgg;
using ShopManagement.Infrastructure.EfCore;
using ShopManagement.Infrastructure.EfCore.Repository;
using System;

namespace ShopManagement.Configuration
{
    public class ShopManagementBootstrapper
    {
        public static void Configure(IServiceCollection Services,string connectionstring)
        {
            Services.AddScoped<IProductCategoryApplication,ProductCategoryApplication>();
            Services.AddTransient<IProductCategoryApplication, ProductCategoryApplication>();
            Services.AddScoped<IProductCategoryRepository, ProductCategoryRepository>();
            Services.AddTransient<IProductCategoryRepository, ProductCategoryRepository>();
            Services.AddDbContext<ShopContext>(x=>x.UseSqlServer(connectionstring));
        }
    }
}
