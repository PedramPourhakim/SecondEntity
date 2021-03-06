using _0_Framework.Application;
using _0_Framework.Infrastructure;
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
    public class ProductCategoryRepository : RepositoryBase<long,ProductCategory>,IProductCategoryRepository
    {
        private readonly ShopContext context;

        public ProductCategoryRepository(ShopContext context) :base(context)
        {
            this.context = context;
        }

        public EditProductCategory GetDetails(long id)
        {
            return context.ProductCategories.Select(x => new EditProductCategory { 
                Id=x.Id,
                Description=x.Description,
                Name=x.Name,
                Keywords=x.Keywords,
                MetaDescription=x.MetaDescription,
                //Picture=x.Picture,
                PictureAlt=x.PictureAlt,
                PictureTitle=x.PictureTitle,
                Slug=x.Slug
            }).FirstOrDefault(x=>x.Id==id);
        }

        public List<ProductCategoryViewModel> GetProductCategories()
        {
            return context.ProductCategories.Select(x => new ProductCategoryViewModel
            {
                Id=x.Id,
                Name=x.Name
            }).ToList();
        }

        public string GetSlugById(long id)
        {
            return context.ProductCategories.Select(x => new { x.Id, x.Slug }).FirstOrDefault(x => x.Id == id).Slug;
        }

        public List<ProductCategoryViewModel> Search(ArticleCategoriesSearchmodel searchModel)
        {
            var query = context.ProductCategories.Select(x => new ProductCategoryViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Picture = x.Picture,
                CreationDate = x.CreationDate.ToFarsi()
            }) ;
            if (!string.IsNullOrWhiteSpace(searchModel.Name))
                query = query.Where(x => x.Name.Contains(searchModel.Name));
            return query.OrderByDescending(x => x.Id).ToList();
        }
    }
}
