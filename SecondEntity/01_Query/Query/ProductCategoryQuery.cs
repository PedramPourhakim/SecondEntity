using _01_Query.Contract.Product;
using _01_Query.Contract.ProductCategory;
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

        public ProductCategoryQuery(ShopContext context)
        {
            this.context = context;
        }

        public List<ProductCategoryQueryModel> GetProductCategories()
        {
            return context.ProductCategories.Select(x =>
            new ProductCategoryQueryModel
            {
                Id=x.Id,
                Name=x.Name,
                Picture=x.Picture,
                PictureAlt=x.PictureAlt,
                PictureTitle=x.PictureTitle,
                Slug=x.Slug
            }).ToList();
        }

        public List<ProductCategoryQueryModel> GetProductCategoriesWithProducts()
        {
            return context.ProductCategories.Include
                (x => x.Products).ThenInclude(x => x.Category)
                .Select(x =>
                  new ProductCategoryQueryModel
                  {
                      Id=x.Id,
                      Name=x.Name,
                      Products=MapProducts(x.Products)
                  }).ToList();
        }

        private static List<ProductQueryModel> MapProducts(List<Product> products)
        {
            var result = new List<ProductQueryModel>();
            foreach (var product in products)
            {
                var item = new ProductQueryModel
                {
                    Id = product.Id,
                    Picture = product.Picture,
                    PictureAlt = product.PictureAlt,
                    PictureTitle = product.PictureTitle,
                    Name = product.Name,
                    Category = product.Category.Name,
                    Slug = product.Slug
                };
                result.Add(item);
            }

            return result;
        }
    }
}
