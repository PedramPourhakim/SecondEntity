﻿using Microsoft.EntityFrameworkCore;
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
        public static void Configure(IServiceCollection services,string connectionstring)
        {
            services.AddScoped<IProductCategoryApplication,ProductCategoryApplication>();
            services.AddTransient<IProductCategoryApplication, ProductCategoryApplication>();
            services.AddScoped<IProductCategoryRepository, ProductCategoryRepository>();
            services.AddTransient<IProductCategoryRepository, ProductCategoryRepository>();
            services.AddDbContext<ShopContext>(x=>x.UseSqlServer(connectionstring));
        }
    }
}