using _0_Framework.Application;
using _01_Query.Contract.Product;
using DiscountManagement.Infrastructure.EfCore;
using InventoryManagement.Infrastructure.EfCore;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Infrastructure.EfCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_Query.Query
{
    public class ProductQuery : IProductQuery
    {
        private readonly ShopContext shopContext;
        private readonly InventoryContext inventoryContext;
        private readonly DiscountContext discountContext;

        public ProductQuery(DiscountContext discountContext,InventoryContext inventoryContext,ShopContext shopContext)
        {
            this.discountContext = discountContext;
            this.inventoryContext = inventoryContext;
            this.shopContext = shopContext;
        }

        public List<ProductQueryModel> GetLatestArrivals()
        {
            var inventory = inventoryContext.Inventory
                 .Select(x => new
                 {
                     x.ProductId,
                     x.UnitPrice
                 }).ToList();

            var discounts = discountContext.CustomerDiscounts
                .Where(x => x.StartDate < DateTime.Now
                && x.EndDate > DateTime.Now
                ).Select(x => new
                {
                    x.DiscountRate,
                    x.ProductId
                }).ToList();
            var products = shopContext.Products
                .Include(x=>x.Category).Select
                (product => new ProductQueryModel
                {
                    Id=product.Id,
                    Picture=product.Picture,
                    PictureAlt=product.PictureAlt,
                    PictureTitle=product.PictureTitle,
                    Name=product.Name,
                    Slug=product.Slug,
                    Category=product.Category.Name,
                    CategorySlug=product.Category.Slug
                }).OrderByDescending(x=>x.Id).Take(6).ToList();
            foreach (var product in products)
            {
               
                var ProductInventory = inventory.FirstOrDefault
                    (x => x.ProductId == product.Id);
                if (ProductInventory != null)
                {
                    var Price = ProductInventory.UnitPrice;
                    product.Price = Price.ToMoney();
                    var discount = discounts
                   .FirstOrDefault(x =>
                   x.ProductId == product.Id);
                    if (discount != null)
                    {
                        int DiscountRate = discount.DiscountRate;
                        product.DiscountRate = DiscountRate;
                        product.HasDiscount = DiscountRate > 0;
                        var DiscountAmount = Math.Round(
                            (Price * DiscountRate) / 100);
                        product.PriceWithDiscount
                            = (Price - DiscountAmount).ToMoney();
                    }
                }


            }
            return products;
        }
    }
}
