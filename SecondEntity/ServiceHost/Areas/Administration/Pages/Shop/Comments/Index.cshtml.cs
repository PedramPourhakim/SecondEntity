using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Comment;
using ShopManagement.Application.Contracts.ProductContracts;
using ShopManagement.Application.Contracts.ProductPictureContracts;
using ShopManagement.Application.Contracts.SlideContracts;
using System.Collections.Generic;

namespace ServiceHost.Areas.Administration.Pages.Shop.Comments
{
    public class IndexModel : PageModel
    {
        [TempData]
        public string Message { get; set; }
        public List<CommentViewModel> Comments;
        public CommentSearchModel SearchModel;
        private readonly ICommentApplication commentApplication;

        public IndexModel(ICommentApplication commentApplication)
        {
            this.commentApplication = commentApplication;
        }

        public void OnGet(CommentSearchModel searchModel)
        {
            Comments = commentApplication.Search(searchModel);
        }
        public IActionResult OnGetCancel(long id)
        {
            var result = commentApplication.Cancel(id);
            if (result.Movafagh)
                return RedirectToPage("./Index");

            Message = result.Message;
            return RedirectToPage("./Index");
        }

        public IActionResult OnGetConfirm(long id)
        {
            var result = commentApplication.Confirm(id);
            if (result.Movafagh)
                return RedirectToPage("./Index");

            Message = result.Message;
            return RedirectToPage("./Index");
        }
    }
}
