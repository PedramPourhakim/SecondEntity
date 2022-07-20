using _0_Framework.Infrastructure;
using _01_Query.Contract.Inventory;
using _01_Query.Query;
using InventoryManagement.Application;
using InventoryManagement.Application.Contracts.Inventory;
using InventoryManagement.Domain.InventoryAgg;
using InventoryManagement.Infrastructure.Configuration.Permissions;
using InventoryManagement.Infrastructure.EfCore;
using InventoryManagement.Infrastructure.EfCore.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace InventoryManagement.Infrastructure.Configuration
{
    public class InventoryManagementBootstrapper
    {
        public static void Configure(IServiceCollection
            services,string connectionstring)
        {
            services.AddTransient<IInventoryRepository,InventoryRepository>();
            services.AddScoped<IInventoryRepository, InventoryRepository>();

            services.AddTransient<IInventoryApplication, InventoryApplication>();
            services.AddScoped<IInventoryApplication, InventoryApplication>();

            services.AddTransient<IPermissionExposer, InventoryPermissionExposer>();
            services.AddScoped<IPermissionExposer, InventoryPermissionExposer>();

            services.AddScoped<IInventoryQuery,InventoryQuery>();
            services.AddTransient<IInventoryQuery,InventoryQuery>();

            services.AddDbContext<InventoryContext>(x=>x.UseSqlServer(connectionstring));
        }
    }
}
