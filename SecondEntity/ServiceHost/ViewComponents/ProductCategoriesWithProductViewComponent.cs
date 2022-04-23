using _01_Query.Contract.ProductCategory;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceHost.ViewComponents
{
    public class ProductCategoriesWithProductViewComponent :ViewComponent
    {
        private readonly IProductCategoryQuery productCategoryQuery;

        public ProductCategoriesWithProductViewComponent(IProductCategoryQuery productCategoryQuery)
        {
            this.productCategoryQuery = productCategoryQuery;
        }

        public IViewComponentResult Invoke()
        {
            var categories = productCategoryQuery
                .GetProductCategoriesWithProducts();
            return View(categories);
        }
    }
}
