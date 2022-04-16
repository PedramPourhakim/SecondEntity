using DiscountManagement.Application;
using DiscountManagement.Application.Contracts.ColleagueDiscount;
using DiscountManagement.Application.Contracts.CustomerDiscount;
using DiscountManagement.Domain.ColleagueDiscountAgg;
using DiscountManagement.Domain.CustomerDiscountAgg;
using DiscountManagement.Infrastructure.EfCore;
using DiscountManagement.Infrastructure.EfCore.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DiscountManagement.Configuration
{
    public class DiscountManagementBootstrapper
    {
        public static void Configure(IServiceCollection services,string connectionstring)
        {
            services.AddScoped<ICustomerDiscountApplication, CustomerDiscountApplication>();
            services.AddTransient<ICustomerDiscountApplication, CustomerDiscountApplication>();

            services.AddScoped<ICustomerDiscountRepository, CustomerDiscountRepository>();
            services.AddTransient<ICustomerDiscountRepository, CustomerDiscountRepository>();

            services.AddScoped<IColleagueDiscountApplication, ColleagueDiscountApplication>();
            services.AddTransient<IColleagueDiscountApplication, ColleagueDiscountApplication>();

            services.AddScoped<IColleagueDiscountRepository, ColleagueDiscountRepository>();
            services.AddTransient<IColleagueDiscountRepository, ColleagueDiscountRepository>();

            services.AddDbContext<DiscountContext>(x => x.UseSqlServer(connectionstring));

        }
    }
}
