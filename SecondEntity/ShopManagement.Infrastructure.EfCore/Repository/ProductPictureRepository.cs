using _0_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contracts.ProductPictureContracts;
using ShopManagement.Domain.ProductPictureAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagement.Infrastructure.EfCore.Repository
{
    public class ProductPictureRepository : RepositoryBase<long, ProductPicture>, IProductPictureRepository
    {
        private readonly ShopContext context;

        public ProductPictureRepository(ShopContext context) : base(context)
        {
            this.context = context;
        }

        public EditProductPicture GetDetails(long id)
        {
            return context.ProductPictures.Select(x => new EditProductPicture
            {
                Id = x.Id,
                ProductId = x.ProductId,
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle
            }).FirstOrDefault(x => x.Id == id);
        }

        public List<ProductPictureViewModel> Search(ProductPictureSearchModel searchModel)
        {
            var query = context.ProductPictures
                .Include(x => x.Product)
                .Select(x => new ProductPictureViewModel
                {
                    Id = x.Id,
                    Product = x.Product.Name,
                    CreationDate=x.CreationDate.ToString(),
                    Picture=x.Picture,
                    ProductId=x.ProductId,
                    IsRemoved=x.IsRemoved
                });
            if (searchModel.ProductId != 0)
                query = query.Where(x => x.ProductId == searchModel.ProductId);
            return query.OrderByDescending(x => x.Id).ToList();
        }
    }
}
