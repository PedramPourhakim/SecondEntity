using AccountManagement.Domain.AccountAgg;
using AccountManagement.Domain.RoleAgg;
using AccountManagement.Infrastructure.EfCore.Mappings;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountManagement.Infrastructure.EfCore
{
   
        public class AccountContext : DbContext
        {

            public DbSet<Account> Accounts { get; set; }
            public DbSet<Role> Roles { get; set; }

            public AccountContext(DbContextOptions<AccountContext> options) : base(options)
            {

            }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                var assembly = typeof(AccountMapping).Assembly;
                modelBuilder.ApplyConfigurationsFromAssembly(assembly);
                base.OnModelCreating(modelBuilder);
            }
        }
    }

