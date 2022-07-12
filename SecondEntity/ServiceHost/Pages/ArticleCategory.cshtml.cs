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
    public class ArticleCategoryModel : PageModel
    {
        public ArticleCategoryQueryModel ArticleCategory;
        public List<ArticleCategoryQueryModel> ArticleCategories;
        public List<ArticleQueryModel> LatestArticles;
        private readonly IArticleCategoryQuery articleCategoryQuery;
        private readonly IArticleQuery articleQuery;
        public ArticleCategoryModel(IArticleCategoryQuery articleCategoryQuery,IArticleQuery articleQuery)
        {
            this.articleCategoryQuery = articleCategoryQuery;
            this.articleQuery = articleQuery;
        }
        public void OnGet(string id)
        {
            LatestArticles = articleQuery.LatestArticles();
            ArticleCategories = articleCategoryQuery.GetArticleCategories();
            ArticleCategory = articleCategoryQuery.GetArticleCategory(id);
        }
    }
}
