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
    public class CreateModel : PageModel
    {
        public SelectList ArticleCategories;
        public CreateArticle command;
        private readonly IArticleCategoryApplication articleCategoryApplication;
        private readonly IArticleApplication articleApplication;
        public CreateModel(IArticleCategoryApplication articleCategoryApplication,IArticleApplication articleApplication)
        {
            this.articleCategoryApplication = articleCategoryApplication;
            this.articleApplication = articleApplication;
        }

        public void OnGet()
        {
            ArticleCategories = new SelectList(articleCategoryApplication.GetArticleCategories(),"Id","Name");
        }
        public IActionResult OnPost(CreateArticle command)
        {
           var result= articleApplication.Create(command);
            return RedirectToPage("./Index");
        }
    }
}
