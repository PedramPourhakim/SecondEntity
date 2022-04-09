using _0_Framework.Application;
using _0_Framework.Infrastructure;
using DiscountManagement.Application.Contracts.CustomerDiscount;
using DiscountManagement.Domain.CustomerDiscountAgg;
using ShopManagement.Infrastructure.EfCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DiscountManagement.Infrastructure.EfCore.Repository
{
    public class CustomerDiscountRepository : RepositoryBase<long, CustomerDiscount>, ICustomerDiscountRepository
    {
        private readonly DiscountContext context;
        private readonly ShopContext shopContext;

        public CustomerDiscountRepository(DiscountContext context,ShopContext shopContext) : base(context)
        {
            this.context = context;
            this.shopContext = shopContext;
        }

        public EditCustomerDiscount GetDetails(long id)
        {
            return context.CustomerDiscounts.Select(x => new
            EditCustomerDiscount
            {
                Id = x.Id,
                ProductId = x.ProductId,
                DiscountRate = x.DiscountRate,
                StartDate = x.StartDate.ToString(),
                EndDate = x.EndDate.ToString(),
                Reason = x.Reason
            }).FirstOrDefault(x => x.Id == id);
        }

        public List<CustomerDiscountViewModel> Search(CustomerDiscountSearchModel searchModel)
        {
            var products = shopContext.Products
                .Select(x => new { x.Id, x.Name }).ToList();
            var query = context.CustomerDiscounts
                .Select(x => new CustomerDiscountViewModel
                {
                    Id = x.Id,
                    ProductId = x.ProductId,
                    EndDate = x.EndDate.ToFarsi(),
                    StartDate = x.StartDate.ToFarsi(),
                    EndDateGr=x.EndDate,
                    StartDateGr=x.StartDate,
                    DiscountRate = x.DiscountRate,
                    Reason = x.Reason,
                   CreationDate=x.CreationDate.ToFarsi()
                });
            if (searchModel.ProductId > 0)
                query = query.Where(x =>
                x.ProductId == searchModel.ProductId);
            if (!string.IsNullOrWhiteSpace(searchModel.StartDate))
            {
                query = query.Where(x => x.StartDateGr >
                searchModel.StartDate.ToGeorgianDateTime());
            }
            if (!string.IsNullOrWhiteSpace(searchModel.EndDate))
            {
                query = query.
                    Where(x => x.EndDateGr < searchModel.EndDate.
                    ToGeorgianDateTime());
            }
            var discounts = query.OrderByDescending(x => x.Id)
                .ToList();
            discounts.ForEach(discount => discount.Product =
            products.FirstOrDefault(x =>
            x.Id == discount.ProductId)?.Name);
            return discounts;
        }
    }
}
