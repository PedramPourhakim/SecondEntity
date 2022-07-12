using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _01_Query.Contract.Article;
using _01_Query.Contract.ArticleCategory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class ArticleModel : PageModel
    {
        public List<ArticleQueryModel> LatestArticles;
        public List<ArticleCategoryQueryModel> ArticleCategories;
        public ArticleQueryModel Article;
        private readonly IArticleQuery articleQuery;
        private readonly IArticleCategoryQuery articleCategoryQuery;
        public ArticleModel(IArticleQuery articleQuery,IArticleCategoryQuery articleCategoryQuery)
        {
            this.articleQuery = articleQuery;
            this.articleCategoryQuery = articleCategoryQuery;
        }

        public void OnGet(string id)
        {
            Article = articleQuery.GetArticleDetails(id);
            LatestArticles = articleQuery.LatestArticles();
            ArticleCategories = articleCategoryQuery.GetArticleCategories();
        }
    }
}
