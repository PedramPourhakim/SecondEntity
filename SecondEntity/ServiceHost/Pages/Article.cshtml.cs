using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _01_Query.Contract.Article;
using _01_Query.Contract.ArticleCategory;
using CommentManagement.Application;
using CommentManagement.Application.Contracts.Comment;
using CommentManagement.Infrastructure.EfCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class ArticleModel : PageModel
    {
        public List<ArticleQueryModel> LatestArticles;
        public List<ArticleCategoryQueryModel> ArticleCategories;
        public ArticleQueryModel Article;
        private readonly ICommentApplication commentApplication;
        private readonly IArticleQuery articleQuery;
        private readonly IArticleCategoryQuery articleCategoryQuery;
        public ArticleModel(IArticleQuery articleQuery
            ,IArticleCategoryQuery articleCategoryQuery,
            ICommentApplication commentApplication)
        {
            this.articleQuery = articleQuery;
            this.articleCategoryQuery = articleCategoryQuery;
            this.commentApplication = commentApplication;
        }

        public void OnGet(string id)
        {
            Article = articleQuery.GetArticleDetails(id);
            LatestArticles = articleQuery.LatestArticles();
            ArticleCategories = articleCategoryQuery.GetArticleCategories();
        }
        public IActionResult OnPost(AddComment command, string articleSlug)
        {
            command.Type = CommentType.Article;
            var result = commentApplication.Add(command);
            return RedirectToPage("/Article", new { Id = articleSlug });
        }
    }
}
