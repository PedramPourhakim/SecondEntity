using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contracts.ProductCategoryContracts;
using System.Collections.Generic;
using _0_Framework.Infrastructure;

namespace ServiceHost.Areas.Administration.Pages.Shop.ProductCategories
{
    [Authorize(Roles = "1,2,3")]
    public class IndexModel : PageModel
    {
        public ArticleCategoriesSearchmodel SearchModel;
        public List<ProductCategoryViewModel> ProductCategories;

        private readonly IProductCategoryApplication
            _productCategoryApplication;

        public IndexModel(IProductCategoryApplication 
            productCategoryApplication)
        {
            _productCategoryApplication = productCategoryApplication;
        }

        public void OnGet(ArticleCategoriesSearchmodel searchModel)
        {
            ProductCategories = _productCategoryApplication.Search
                (searchModel);
        }

        public IActionResult OnGetCreate()
        {
            return Partial("./Create", new CreateProductCategory());
        }

        public JsonResult OnPostCreate(CreateProductCategory command)
        {
            var result = _productCategoryApplication.Create(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
            var productCategory = _productCategoryApplication
                .GetDetails(id);
            return Partial("Edit", productCategory);
        }

        public JsonResult OnPostEdit(EditProductCategory command)
        {
            
            var result = _productCategoryApplication.Edit(command);
            return new JsonResult(result);
        }
    }
}