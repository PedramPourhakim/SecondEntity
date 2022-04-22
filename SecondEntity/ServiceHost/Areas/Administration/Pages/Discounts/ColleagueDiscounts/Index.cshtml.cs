using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiscountManagement.Application.Contracts.ColleagueDiscount;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.ProductCategoryContracts;
using ShopManagement.Application.Contracts.ProductContracts;

namespace ServiceHost.Areas.Administration.Pages.Discounts.ColleagueDiscounts
{
    public class IndexModel : PageModel
    {
        [TempData]
        public string Message { get; set; }
        public List<ColleagueDiscountViewModel> ColleagueDiscounts;
        public InventoryDearchModel SearchModel;
        public SelectList Products;
        private readonly IProductApplication productApplication;
        private readonly IColleagueDiscountApplication ColleagueDiscountApplication;
        public IndexModel(IProductApplication productApplication, IColleagueDiscountApplication ColleagueDiscountApplication)
        {
            this.ColleagueDiscountApplication = ColleagueDiscountApplication;
            this.productApplication = productApplication;
        }
        public void OnGet(InventoryDearchModel searchModel)
        {
            Products = new SelectList(productApplication.GetProducts(), "Id", "Name");//مقدار اسم را از لیست بگیر ونمایش بده و مقدار آیدی هم داخل آیدی قرارا بده
            ColleagueDiscounts = ColleagueDiscountApplication.Search(searchModel);
        }
        public IActionResult OnGetCreate()
        {
            var command = new DefineColleagueDiscount
            {
                Products = (productApplication.GetProducts())
            };
            return Partial("./Create", command);
        }
        public JsonResult OnPostCreate(DefineColleagueDiscount command)
        {
            var result = ColleagueDiscountApplication.Define(command);
            return new JsonResult(result);
        }
        public IActionResult OnGetEdit(long id)
        {
            var ColleagueDiscount = ColleagueDiscountApplication
                .GetDetails(id);
            ColleagueDiscount.Products = productApplication
                .GetProducts();
            return Partial("./Edit", ColleagueDiscount);
        }
        public JsonResult OnPostEdit(EditColleagueDiscount command)
        {
            var result = ColleagueDiscountApplication.Edit(command);
            return new JsonResult(result);
        }
        public IActionResult OnGetRemove(long id)
        {
            ColleagueDiscountApplication.Remove(id);
            return RedirectToPage("./Index");
        }
        public IActionResult OnGetRestore(long id)
        {
            ColleagueDiscountApplication.Restore(id);
            return RedirectToPage("./Index");
        }

    }
}
