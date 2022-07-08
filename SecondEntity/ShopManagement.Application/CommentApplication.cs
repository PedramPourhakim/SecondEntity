using _0_Framework.Application;
using ShopManagement.Application.Contracts.Comment;
using ShopManagement.Domain.CommentAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagement.Application
{
    public class CommentApplication : ICommentApplication
    {
        private readonly ICommentRepository commentRepository;

        public CommentApplication(ICommentRepository commentRepository)
        {
            this.commentRepository = commentRepository;
        }

        public OperationResult Add(AddComment command)
        {
            var operation = new OperationResult();
            var comment = new Comment(command.Name,
                command.Email,command.Message,
                command.ProductId);
            commentRepository.Create(comment);
            commentRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Cancel(long id)
        {
            var operation = new OperationResult();
            var comment = commentRepository.Get(id);
            if (comment == null)
                return operation.Failed(ApplicationMessages
                    .RecordNotFound);
            comment.Cancel();
            commentRepository.SaveChanges();
            return operation.Succeeded();    
        }

        public OperationResult Confirm(long id)
        {
            var operation = new OperationResult();
            var comment = commentRepository.Get(id);
            if (comment == null)
                return operation.Failed(ApplicationMessages
                    .RecordNotFound);
            comment.Confirm();
            commentRepository.SaveChanges();
            return operation.Succeeded();
        }

        public List<CommentViewModel> Search(CommentSearchModel searchModel)
        {
            return commentRepository.Search(searchModel);
        }
    }
}
