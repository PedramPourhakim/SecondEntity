using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogManagement.Application.Contracts.Article;
using BlogManagement.Application.Contracts.ArticleCategory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServiceHost.Areas.Administration.Pages.Blog.Articles
{
    public class EditModel : PageModel
    {
        public SelectList ArticleCategories;
        public EditArticle command;
        private readonly IArticleCategoryApplication articleCategoryApplication;
        private readonly IArticleApplication articleApplication;
        public EditModel(IArticleCategoryApplication articleCategoryApplication, IArticleApplication articleApplication)
        {
            this.articleCategoryApplication = articleCategoryApplication;
            this.articleApplication = articleApplication;
        }

        public void OnGet(long id)
        {
            command = articleApplication.GetDetails(id);
            ArticleCategories = new SelectList(articleCategoryApplication.GetArticleCategories(), "Id", "Name");
        }
        public IActionResult OnPost(EditArticle command)
        {
            var result = articleApplication.Edit(command);
            return RedirectToPage("./Index");
        }
    }
}
