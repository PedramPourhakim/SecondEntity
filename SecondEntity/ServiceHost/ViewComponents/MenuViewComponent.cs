using _01_Query.Contract.ProductCategory;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceHost.ViewComponents
{
    public class MenuViewComponent :ViewComponent
    {
        private readonly IProductCategoryQuery productCategoryQuery;

        public MenuViewComponent(IProductCategoryQuery productCategoryQuery)
        {
            this.productCategoryQuery = productCategoryQuery;
        }

        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
