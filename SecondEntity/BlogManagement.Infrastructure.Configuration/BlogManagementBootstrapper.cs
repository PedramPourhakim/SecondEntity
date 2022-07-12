using _01_Query.Contract.Article;
using _01_Query.Contract.ArticleCategory;
using _01_Query.Query;
using BlogManagement.Application;
using BlogManagement.Application.Contracts.Article;
using BlogManagement.Application.Contracts.ArticleCategory;
using BlogManagement.Domain.ArticleAgg;
using BlogManagement.Domain.ArticleCategoryAgg;
using BlogManagement.Infrastructure.EfCore;
using BlogManagement.Infrastructure.EfCore.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace BlogManagement.Infrastructure.Configuration
{
    public class BlogManagementBootstrapper
    {
        public static void Configure(IServiceCollection services,string ConnectionString)
        {
            services.AddTransient<IArticleCategoryApplication, ArticleCategoryApplication>();
            services.AddScoped<IArticleCategoryApplication, ArticleCategoryApplication>();

            services.AddTransient<IArticleCategoryRepository, ArticleCategoryRepository>();
            services.AddScoped<IArticleCategoryRepository, ArticleCategoryRepository>();

            services.AddTransient<IArticleApplication, ArticleApplication>();
            services.AddScoped<IArticleApplication, ArticleApplication>();

            services.AddTransient<IArticleRepository, ArticleRepository>();
            services.AddScoped<IArticleRepository, ArticleRepository>();

            services.AddTransient<IArticleQuery,ArticleQuery>();
            services.AddScoped<IArticleQuery,ArticleQuery>();

            services.AddTransient<IArticleCategoryQuery, ArticleCategoryQuery>();
            services.AddScoped<IArticleCategoryQuery, ArticleCategoryQuery>();

            services.AddDbContext<BlogContext>(x=>x.UseSqlServer(ConnectionString));
        }
    }
}
