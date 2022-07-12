using _0_Framework.Application;
using _0_Framework.Infrastructure;
using CommentManagement.Application.Contracts.Comment;
using CommentManagement.Domain.CommentAgg;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommentManagement.Infrastructure.EfCore.Repository
{
    public class CommentRepository : RepositoryBase<long, Comment>, ICommentRepository
    {
        private readonly CommentContext CommentContext;
        public CommentRepository(CommentContext CommentContext) :base(CommentContext)
        {
            this.CommentContext = CommentContext;
        }
        public List<CommentViewModel> Search(CommentSearchModel searchModel)
        {
            var query = CommentContext.Comments.
                Select(x => new CommentViewModel
                {
                    Id=x.Id,
                    Name = x.Name,
                    Email = x.Email,
                    Website = x.Website,
                    Message = x.Message,
                    OwnerRecordID=x.OwnerRecordID,
                    IsConfirmed = x.IsConfirmed,
                    IsCanceled=x.IsCanceled,
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
