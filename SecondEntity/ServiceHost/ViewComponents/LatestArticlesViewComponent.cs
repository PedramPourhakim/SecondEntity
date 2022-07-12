using _01_Query.Contract.Article;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceHost.ViewComponents
{
    public class LatestArticlesViewComponent :ViewComponent
    {
        private readonly IArticleQuery articleQuery;

        public LatestArticlesViewComponent(IArticleQuery articleQuery)
        {
            this.articleQuery = articleQuery;
        }

        public IViewComponentResult Invoke()
        {
            var articles = articleQuery
                .LatestArticles();
            return View(articles);
        }
    }
}
