using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagement.Infrastructure.EfCore.Repository
{
    public class Test
    {
        private readonly AccountContext accountContext;
        public Test(AccountContext accountContext)
        {
            this.accountContext = accountContext;
        }
    }
}
