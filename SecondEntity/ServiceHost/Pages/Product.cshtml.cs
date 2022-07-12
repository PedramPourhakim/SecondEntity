using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _01_Query.Contract.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CommentManagement.Application.Contracts.Comment;

namespace ServiceHost.Pages
{
    public class ProductModel : PageModel
    {
        public ProductQueryModel Product;
        private readonly IProductQuery productQuery;
        private readonly ICommentApplication commentApplication;
        public ProductModel(IProductQuery productQuery,ICommentApplication commentApplication)
        {
            this.productQuery = productQuery;
            this.commentApplication = commentApplication;
        }
        public void OnGet(string id)
        {
            Product = productQuery.GetDetails(id);
        }
        public IActionResult OnPost(AddComment command,string ProductSlug)
        {
            var result = commentApplication.Add(command);
            return RedirectToPage("/Product",new {Id=ProductSlug });
        }
    }
}
