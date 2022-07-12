
using _01_Query;
using _01_Query.Contract.ArticleCategory;
using _01_Query.Contract.ProductCategory;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace ServiceHost.ViewComponents
{
    public class MenuViewComponent : ViewComponent
    {
        private readonly IProductCategoryQuery productCategoryQuery;
        private readonly IArticleCategoryQuery articleCategoryQuery;
        public MenuViewComponent(IProductCategoryQuery productCategoryQuery, IArticleCategoryQuery articleCategoryQuery)
        {
            this.articleCategoryQuery = articleCategoryQuery;
            this.productCategoryQuery = productCategoryQuery;
        }

        public IViewComponentResult Invoke()
        {
            var result = new MenuModel
            {
                ArticleCategories = articleCategoryQuery.GetArticleCategories(),
                ProductCategories = productCategoryQuery.GetProductCategories()
            };
            return View(result);
        }
    }
}
