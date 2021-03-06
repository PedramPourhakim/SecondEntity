using _01_Query.Contract;
using _01_Query.Contract.Product;
using _01_Query.Contract.ProductCategory;
using _01_Query.Contract.Slide;
using _01_Query.Query;
using CommentManagement.Application;
using CommentManagement.Application.Contracts.Comment;
using CommentManagement.Domain.CommentAgg;
using CommentManagement.Infrastructure.EfCore.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShopManagement.Application;
using ShopManagement.Application.Contracts.Order;
using ShopManagement.Application.Contracts.ProductCategoryContracts;
using ShopManagement.Application.Contracts.ProductContracts;
using ShopManagement.Application.Contracts.ProductPictureContracts;
using ShopManagement.Application.Contracts.SlideContracts;
using ShopManagement.Domain.OrderAgg;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductCategoryAgg;
using ShopManagement.Domain.ProductPictureAgg;
using ShopManagement.Domain.Services;
using ShopManagement.Domain.SliderAgg;
using ShopManagement.Infrastructure.AccountAcl;
using ShopManagement.Infrastructure.EfCore;
using ShopManagement.Infrastructure.EfCore.Repository;
using ShopManagement.Infrastructure.InventoryAcl;
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


            services.AddScoped<IProductApplication, ProductApplication>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddTransient<IProductApplication, ProductApplication>();
            services.AddTransient<IProductRepository, ProductRepository>();

            services.AddTransient<IProductPictureRepository, ProductPictureRepository>();
            services.AddTransient<IProductPictureApplication, ProductPictureApplication>();
            services.AddScoped<IProductPictureApplication, ProductPictureApplication>();
            services.AddScoped<IProductPictureRepository, ProductPictureRepository>();

            services.AddTransient<ISlideApplication, SlideApplication>();
            services.AddTransient<ISlideRepository, SlideRepository>();
            services.AddScoped<ISlideApplication, SlideApplication>();
            services.AddScoped<ISlideRepository, SlideRepository>();

            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddTransient<IOrderApplication, OrderApplication>();
            services.AddScoped<IOrderApplication, OrderApplication>();

            services.AddSingleton<ICartService, CartService>();

            services.AddScoped<IShopInventoryAcl,ShopInventoryAcl>();
            services.AddTransient<IShopInventoryAcl,ShopInventoryAcl>();

            services.AddTransient<ISlideQuery, SlideQuery>();
            services.AddScoped<ISlideQuery, SlideQuery>();

            services.AddTransient<IProductCategoryQuery, ProductCategoryQuery>();
            services.AddScoped<IProductCategoryQuery, ProductCategoryQuery>();

            services.AddTransient<IProductQuery, ProductQuery>();
            services.AddScoped<IProductQuery, ProductQuery>();

            services.AddTransient<ICartCalculatorService, CartCalculateService>();
            services.AddScoped<ICartCalculatorService, CartCalculateService>();

            services.AddScoped<IShopAccountAcl, ShopAccountAcl>();
            services.AddTransient<IShopAccountAcl, ShopAccountAcl>();

            services.AddDbContext<ShopContext>(x=>x.UseSqlServer(connectionstring));
        }
    }
}
