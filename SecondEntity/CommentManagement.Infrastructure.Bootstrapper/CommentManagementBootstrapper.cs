using CommentManagement.Application;
using CommentManagement.Application.Contracts.Comment;
using CommentManagement.Domain.CommentAgg;
using CommentManagement.Infrastructure.EfCore;
using CommentManagement.Infrastructure.EfCore.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace CommentManagement.Infrastructure.Bootstrapper
{
    public class CommentManagementBootstrapper
    {
        public static void Configure
            (IServiceCollection services,
            string connectionString)
        {
            services.AddTransient<ICommentRepository,CommentRepository>();
            services.AddScoped<ICommentRepository,CommentRepository>();

            services.AddTransient<ICommentApplication, CommentApplication>();
            services.AddScoped<ICommentApplication, CommentApplication>();

            services.AddDbContext<CommentContext>(x=>x.UseSqlServer(connectionString));
        }
    }
}

