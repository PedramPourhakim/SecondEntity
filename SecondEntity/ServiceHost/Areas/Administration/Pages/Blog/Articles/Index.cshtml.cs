using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contracts.ProductCategoryContracts;
using System.Collections.Generic;
using _0_Framework.Infrastructure;
using BlogManagement.Application.Contracts.ArticleCategory;
using BlogManagement.Application.Contracts.Article;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServiceHost.Areas.Administration.Pages.Blog.Articles
{
    //[Authorize(Roles = "1, 3")]
    public class IndexModel : PageModel
    {
        public ArticleSearchModel SearchModel;
        public List<ArticleViewModel> Articles;

        public SelectList ArticleCategories;

        private readonly IArticleCategoryApplication articleCategoryApplication;

        private readonly IArticleApplication articleApplication;

        public IndexModel(IArticleApplication  articleApplication,IArticleCategoryApplication articleCategoryApplication)
        {
            this.articleApplication = articleApplication;
            this.articleCategoryApplication = articleCategoryApplication;
        }

        public void OnGet(ArticleSearchModel searchModel)
        {
            ArticleCategories = new SelectList
                (articleCategoryApplication.
                GetArticleCategories(),"Id","Name");
            Articles = articleApplication.Search(searchModel);
        }

    }
}