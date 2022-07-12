using _0_Framework.Application;
using CommentManagement.Application.Contracts.Comment;
using CommentManagement.Domain.CommentAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommentManagement.Application
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
                command.Email,command.Website,
                command.Message,
                command.OwnerRecordID,command.Type,
                command.ParentID);
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
