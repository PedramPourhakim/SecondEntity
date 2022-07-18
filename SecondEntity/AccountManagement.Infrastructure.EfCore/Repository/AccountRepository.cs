using _0_Framework.Application;
using _0_Framework.Infrastructure;
using AccountManagement.Application.Contracts.Account;
using AccountManagement.Domain.AccountAgg;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountManagement.Infrastructure.EfCore.Repository
{
    public class AccountRepository : RepositoryBase<long, Account>, IAccountRepository
    {
        private readonly AccountContext accountContext;

        public AccountRepository(AccountContext accountContext):base(accountContext)
        {
            this.accountContext = accountContext;
        }

        public List<AccountViewModel> GetAccounts()
        {
            return accountContext.Accounts.Select(x=>
            new AccountViewModel 
            {
                Id=x.Id,
                FullName=x.FullName
            }).ToList();
        }

        public Account GetBy(string username)
        {
            return accountContext.Accounts.FirstOrDefault
                (x=>x.UserName==username);
        }

        public EditAccount GetDetails(long id)
        {
            return accountContext.Accounts.Select
                (x => new EditAccount
                { 
                    Id=x.Id,
                    FullName=x.FullName,
                    Mobile=x.Mobile,
                    RoleId=x.RoleId,
                    UserName=x.UserName
                }).FirstOrDefault(x=>x.Id==id);
        }

        public List<AccountViewModel> Search(AccountSearchModel searchmodel)
        {
            var query = accountContext.Accounts.Include(
                x => x.Role).Select(x => new AccountViewModel
                {
                    Id = x.Id,
                    FullName = x.FullName,
                    Mobile = x.Mobile,
                    ProfilePhoto = x.ProfilePhoto,
                    Role = x.Role.Name,
                    RoleId = x.RoleId,
                    UserName = x.UserName,
                    CreationDate = x.CreationDate.ToFarsi()
                });
            if (!string.IsNullOrWhiteSpace(searchmodel.FullName))
                query = query.Where(x => x.FullName.Contains(searchmodel.FullName));

            if (!string.IsNullOrWhiteSpace(searchmodel.UserName))
                query = query.Where(x => x.UserName.Contains(searchmodel.UserName));

            if (!string.IsNullOrWhiteSpace(searchmodel.Mobile))
                query = query.Where(x => x.Mobile.Contains(searchmodel.Mobile));

            if (searchmodel.RoleId > 0)
                query = query.Where(x => x.RoleId == searchmodel.RoleId);

            return query.OrderByDescending(x => x.Id).ToList();
        }
    }
}
