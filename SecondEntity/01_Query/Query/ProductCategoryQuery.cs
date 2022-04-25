using _0_Framework.Application;
using _01_Query.Contract.Product;
using _01_Query.Contract.ProductCategory;
using DiscountManagement.Infrastructure.EfCore;
using InventoryManagement.Infrastructure.EfCore;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Infrastructure.EfCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_Query.Query
{
    public class ProductCategoryQuery : IProductCategoryQuery
    {
        private readonly ShopContext context;
        private readonly InventoryContext inventoryContext;
        private readonly DiscountContext discountContext;
        public ProductCategoryQuery(ShopContext context, InventoryContext inventoryContext, DiscountContext discountContext)
        {
            this.context = context;
            this.inventoryContext = inventoryContext;
            this.discountContext = discountContext;
        }

        public List<ProductCategoryQueryModel> GetProductCategories()
        {
            return context.ProductCategories.Select(x =>
            new ProductCategoryQueryModel
            {
                Id = x.Id,
                Name = x.Name,
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                Slug = x.Slug
            }).ToList();
        }

        public List<ProductCategoryQueryModel> GetProductCategoriesWithProducts()
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

            var categories = context.ProductCategories.Include
                (x => x.Products).ThenInclude(x => x.Category)
                .Select(x =>
                  new ProductCategoryQueryModel
                  {
                      Id = x.Id,
                      Name = x.Name,
                      Products = MapProducts(x.Products)
                  }).ToList();
            foreach (var category in categories)
            {
                foreach (var product in category.Products)
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
            }

            return categories;
        }

        private static List<ProductQueryModel> MapProducts(List<Product> products)
        {
            return products.Select(product => new ProductQueryModel
            {
                Id = product.Id,
                Picture = product.Picture,
                PictureAlt = product.PictureAlt,
                PictureTitle = product.PictureTitle,
                Name = product.Name,
                Category = product.Category.Name,
                Slug = product.Slug
            }).ToList();
        }

        public ProductCategoryQueryModel GetProductCategoryWithProductsBy(string slug)
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
                    x.ProductId,
                    x.EndDate
                }).ToList();

            var Category = context.ProductCategories.Include
                (x => x.Products).ThenInclude(x => x.Category)
                .Select(x =>
                  new ProductCategoryQueryModel
                  {
                      Id = x.Id,
                      Name = x.Name,
                      Description=x.Description,
                      MetaDescription=x.MetaDescription,
                      KeyWords=x.Keywords,
                      Slug=x.Slug,
                      Products = MapProducts(x.Products)
                  }).FirstOrDefault(x=>x.Slug==slug);
           
                foreach (var product in Category.Products)
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
                        product.DiscountExpireDate = discount.EndDate.ToDiscountFormat();
                            product.HasDiscount = DiscountRate > 0;
                            var DiscountAmount = Math.Round(
                                (Price * DiscountRate) / 100);
                            product.PriceWithDiscount
                                = (Price - DiscountAmount).ToMoney();
                        }
                    }


                }
            return Category;
            }
        }
    
}
