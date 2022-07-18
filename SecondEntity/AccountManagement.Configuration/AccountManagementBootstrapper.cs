using AccountManagement.Application;
using AccountManagement.Application.Contracts.Account;
using AccountManagement.Application.Contracts.Role;
using AccountManagement.Domain.AccountAgg;
using AccountManagement.Domain.RoleAgg;
using AccountManagement.Infrastructure.EfCore;
using AccountManagement.Infrastructure.EfCore.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountManagement.Configuration
{
    public class AccountManagementBootstrapper
    {
        public static void Configure(IServiceCollection services, string connectionString)
        {
            services.AddTransient<IAccountApplication, AccountApplication>();
            services.AddScoped<IAccountApplication, AccountApplication>();
            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();

            services.AddTransient<IRoleApplication, RoleApplication>();
            services.AddScoped<IRoleApplication, RoleApplication>();
            services.AddTransient<IRoleRepository, RoleRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();

            services.AddDbContext<AccountContext>(x => x.UseSqlServer(connectionString));
        }
    }
}
