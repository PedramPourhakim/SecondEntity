using ShopManagement.Application.Contracts.ProductCategoryContracts;
using ShopManagement.Domain.ProductCategoryAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagement.Infrastructure.EfCore.Repository
{
    public class ProductCategoryRepository : IProductCategoryRepository
    {
        private readonly ShopContext context;

        public ProductCategoryRepository(ShopContext context)
        {
            this.context = context;
        }

        public void Create(ProductCategory entity)
        {
            context.ProductCategories.Add(entity);
        }

        public bool Exists(Expression<Func<ProductCategory, bool>> expression)
        {
            return context.ProductCategories.Any(expression);
        }

        public ProductCategory Get(long id)
        {
            return context.ProductCategories.Find(id);
        }

        public List<ProductCategory> GetAll()
        {
            return context.ProductCategories.ToList();
        }

        public EditProductCategory GetDetails(long id)
        {
            return context.ProductCategories.Select(x => new EditProductCategory { 
                Id=x.Id,
                Description=x.Description,
                Name=x.Name,
                Keywords=x.Keywords,
                MetaDescription=x.MetaDescription,
                Picture=x.Picture,
                PictureAlt=x.PictureAlt,
                PictureTitle=x.PictureTitle,
                Slug=x.Slug
            }).FirstOrDefault(x=>x.Id==id);
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }

        public List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel)
        {
            var query = context.ProductCategories.Select(x => new ProductCategoryViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Picture = x.Picture,
                CreationDate = x.CreationDate.ToString()
            }) ;
            if (!string.IsNullOrWhiteSpace(searchModel.Name))
                query = query.Where(x => x.Name.Contains(searchModel.Name));
            return query.OrderByDescending(x => x.Id).ToList();
        }
    }
}
