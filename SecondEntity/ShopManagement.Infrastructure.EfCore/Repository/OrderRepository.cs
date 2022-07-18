using _0_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Domain.OrderAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagement.Infrastructure.EfCore.Repository
{
    public class OrderRepository:RepositoryBase<long,Order>,IOrderRepository
    {
        private readonly ShopContext shopContext;
        public OrderRepository(ShopContext shopContext):base(shopContext)
        {
            this.shopContext = shopContext;
        }
    }

}
