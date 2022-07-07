using _0_Framework.Application;
using _0_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contracts.Comment;
using ShopManagement.Domain.CommentAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagement.Infrastructure.EfCore.Repository
{
    public class CommentRepository : RepositoryBase<long, Comment>, ICommentRepository
    {
        private readonly ShopContext shopContext;
        public CommentRepository(ShopContext shopContext):base(shopContext)
        {
            this.shopContext = shopContext;
        }
        public List<CommentViewModel> Search(CommentSearchModel searchModel)
        {
            var query = shopContext.Comments.
                Include(x=>x.Product)
                .Select(x => new CommentViewModel
                {
                    Id=x.Id,
                    Email=x.Email,
                    IsConfirmed=x.IsConfirmed,
                    IsCanceled=x.IsCanceled,
                    Message=x.Message,
                    Name=x.Name,
                    ProductId=x.ProductId,
                    ProductName=x.Product.Name,
                    CommentDate=x.CreationDate.ToFarsi()
                });
            if (!string.IsNullOrWhiteSpace(searchModel.Name))
                query = query.Where
                    (x => x.Name.Contains(searchModel.Name));

            if (!string.IsNullOrWhiteSpace(searchModel.Email))
                query = query.Where
                    (x => x.Email.Contains(searchModel.Email));

            return query.OrderByDescending
                (x=>x.Id).ToList();
        }
    }
}
