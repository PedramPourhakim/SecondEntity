using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.ProductCategoryContracts;
using ShopManagement.Application.Contracts.ProductContracts;

namespace ServiceHost.Areas.Administration.Pages.Shop.Products
{
    public class IndexModel : PageModel
    {
        [TempData]
        public string Message { get; set; }
        public List<ProductViewModel> Products;
        public ProductSearchModel SearchModel;
        public SelectList ProductCategories;
        private readonly IProductApplication productApplication;
        private readonly IProductCategoryApplication productCategoryApplication;
            public IndexModel(IProductApplication productApplication,IProductCategoryApplication productCategoryApplication)
        {
            this.productApplication = productApplication;
            this.productCategoryApplication = productCategoryApplication;
        }
        public void OnGet(ProductSearchModel searchModel)
        {
            ProductCategories = new SelectList(productCategoryApplication.GetProductCategories(),"Id","Name");//مقدار اسم را از لیست بگیر ونمایش بده و مقدار آیدی هم داخل آیدی قرارا بده
            Products= productApplication.SearchModel(searchModel);
        }
        public IActionResult OnGetCreate()
        {
            var command = new CreateProduct
            {
                Categories = productCategoryApplication.GetProductCategories()
            };
            return Partial("./Create",command);
        }
        public JsonResult OnPostCreate(CreateProduct command)
        {
            var result = productApplication.Create(command);
            return new JsonResult(result);
        }
        public IActionResult OnGetEdit(long id)
        {
            var product = productApplication.GetDetails(id);
            product.Categories = productCategoryApplication.GetProductCategories();
            return Partial("./Edit", product);
        }
        public JsonResult OnPostEdit(EditProduct command)
        {
            var result = productApplication.Edit(command);
            return new JsonResult(result);
        }
        public IActionResult OnGetNotInStock(long id)
        {
            var result=productApplication.NotInStock(id);
            if (result.Movafagh==true)
            {
                return RedirectToPage("./Index");
            }
            Message = result.Message;
            return RedirectToPage("./Index");
        }
        public IActionResult OnGetIsInStock(long id)
        {            
            var result = productApplication.InStock(id);
            if (result.Movafagh == true)
            {
                return RedirectToPage("./Index");
            }
            Message = result.Message;
            return RedirectToPage("./Index");
        }
    }
}
