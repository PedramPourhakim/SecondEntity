using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiscountManagement.Application.Contracts.CustomerDiscount;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.ProductCategoryContracts;
using ShopManagement.Application.Contracts.ProductContracts;

namespace ServiceHost.Areas.Administration.Pages.Discounts.CustomerDiscounts
{
    public class IndexModel : PageModel
    {
        [TempData]
        public string Message { get; set; }
        public List<CustomerDiscountViewModel> CustomerDiscounts;
        public CustomerDiscountSearchModel SearchModel;
        public SelectList Products;
        private readonly IProductApplication productApplication;
        private readonly ICustomerDiscountApplication customerDiscountApplication;
        public IndexModel(IProductApplication productApplication, ICustomerDiscountApplication customerDiscountApplication)
        {
            this.customerDiscountApplication = customerDiscountApplication;
            this.productApplication = productApplication;
        }
        public void OnGet(CustomerDiscountSearchModel searchModel)
        {
            Products = new SelectList(productApplication.GetProducts(), "Id", "Name");//مقدار اسم را از لیست بگیر ونمایش بده و مقدار آیدی هم داخل آیدی قرارا بده
            CustomerDiscounts = customerDiscountApplication.Search(searchModel);
        }
        public IActionResult OnGetCreate()
        {
            var command = new DefineCustomerDiscount
            {
                Products = (productApplication.GetProducts())
            };
            return Partial("./Create",command);
    }
    public JsonResult OnPostCreate(DefineCustomerDiscount command)
    {
        var result = customerDiscountApplication.Define(command);
        return new JsonResult(result);
    }
    public IActionResult OnGetEdit(long id)
    {
        var CustomerDiscount = customerDiscountApplication
            .GetDetails(id);
        CustomerDiscount.Products = productApplication
            .GetProducts();
        return Partial("./Edit", CustomerDiscount);
    }
    public JsonResult OnPostEdit(EditCustomerDiscount command)
    {
        var result = customerDiscountApplication.Edit(command);
        return new JsonResult(result);
    }

}
}
