using _0_Framework.Application;
using _0_Framework.Infrastructure;
using DiscountManagement.Application.Contracts.ColleagueDiscount;
using DiscountManagement.Domain.ColleagueDiscountAgg;
using ShopManagement.Infrastructure.EfCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscountManagement.Infrastructure.EfCore.Repository
{
    public class ColleagueDiscountRepository : RepositoryBase<long, ColleagueDiscount>, IColleagueDiscountRepository
    {
        private readonly DiscountContext context;
        private readonly ShopContext shopContext;
        public ColleagueDiscountRepository(DiscountContext context,ShopContext shopContext):base(context)
        {
            this.context = context;
            this.shopContext = shopContext;
        }

        public EditColleagueDiscount GetDetails(long id)
        {
            return context.ColleagueDiscounts.Select(x =>
            new EditColleagueDiscount 
            {
                Id=x.Id,
                DiscountRate=x.DiscountRate,
                ProductId=x.ProductId
            }).FirstOrDefault(x=>x.Id==id);
        }

        public List<ColleagueDiscountViewModel> Search(ColleagueDiscountSearchModel searchModel)
        {
            var products = shopContext.Products
                .Select(x => new
                { 
                    x.Id,
                    x.Name
                }).ToList();
            var query = context.ColleagueDiscounts.
                Select(x => new ColleagueDiscountViewModel
                {
                    Id=x.Id,
                    CreationDate=x.CreationDate.ToFarsi(),
                    DiscountRate=x.DiscountRate,
                    IsRemoved=x.IsRemoved,
                    ProductId=x.ProductId
                });
            if (searchModel.ProductId > 0)
                query = query.Where(x => x.ProductId
                == searchModel.ProductId);
            var discounts = query.OrderByDescending(x =>
              x.Id).ToList();
            discounts.ForEach(discount =>
              discount.Product = products.FirstOrDefault
              (x=>x.Id==discount.ProductId)?.Name);
            return discounts;
        }
    }
}
