using _0_Framework.Application;
using _0_Framework.Infrastructure;
using AccountManagement.Infrastructure.EfCore;
using ShopManagement.Application.Contracts;
using ShopManagement.Application.Contracts.Order;
using ShopManagement.Domain.OrderAgg;
using System.Collections.Generic;
using System.Linq;

namespace ShopManagement.Infrastructure.EfCore.Repository
{
    public class OrderRepository : RepositoryBase<long, Order>, IOrderRepository
    {
        private readonly AccountContext accountContext;
        private readonly ShopContext shopContext;
        public OrderRepository(ShopContext shopContext
            ,AccountContext accountContext) 
            : base(shopContext)
        {
            this.shopContext = shopContext;
            this.accountContext = accountContext;
        }

        public double GetAmountBy(long id)
        {
            var order = shopContext.Orders.Select(x =>
              new { x.PayAmount, x.Id }).FirstOrDefault(
                x => x.Id == id);
            if (order != null)
                return order.PayAmount;
            return 0;
        }

        public List<OrderItemViewModel> GetItems(long OrderId)
        {
            var products = shopContext.Products.Select
                (x => new {x.Id,x.Name }).ToList();
            var order = shopContext.Orders.FirstOrDefault
                (x=>x.Id==OrderId);
            if (order == null)
                return new List<OrderItemViewModel>();

            var items = order.Items.Select(x => new
              OrderItemViewModel
            {
                Id=x.Id,
                Count=x.Count,
                DiscountRate=x.DiscountRate,
                OrderId=x.OrderId,
                ProductId=x.ProductId,
                UnitPrice=x.UnitPrice
            }).ToList();
            foreach (var item in items)
            {
                item.Product = products.FirstOrDefault
                    (x => x.Id == item.ProductId)?.Name;
            }
            return items;
        }

        public List<OrderViewModel> Search(OrderSearchModel searchModel)
        {

            var accounts = accountContext.Accounts.
                Select(x => new { x.Id, x.FullName })
                .ToList();
            var query = shopContext.Orders.Select(x =>
              new OrderViewModel
              {
                  Id = x.Id,
                  AccountId = x.AccountId,
                  DiscountAmount = x.DiscountAmount,
                  IsCanceled = x.IsCanceled,
                  IsPaid = x.IsPaid,
                  IssueTrackingNo = x.IssueTrackingNo,
                  PayAmount = x.PayAmount,
                  PaymentMethodId = x.PaymentMethod,
                  RefId = x.RefId,
                  TotalAmount = x.TotalAmount,
                  CreationDate = x.CreationDate.ToFarsi(),
              });

            query = query.Where(x => x.IsCanceled ==
              searchModel.IsCanceled);

            if (searchModel.AccountID > 0)
            {
                query = query.Where(x => x.AccountId
                == searchModel.AccountID);
            }

            var orders = query.OrderByDescending(x => x.Id)
                .ToList();
            foreach (var order in orders)
            {
                order.AccountFullName = accounts
                    .FirstOrDefault(x=>x.Id==order.AccountId
                    )?.FullName;
                order.PaymentMethod = PaymentMethod
                    .GetBy(order.PaymentMethodId).Name;
            }
            return orders;
        }
    }

}
