using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiscountManagement.Application.Contracts.ColleagueDiscount;
using InventoryManagement.Application.Contracts.Inventory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.ProductCategoryContracts;
using ShopManagement.Application.Contracts.ProductContracts;

namespace ServiceHost.Areas.Administration.Pages.Inventory
{
    public class IndexModel : PageModel
    {
        [TempData]
        public string Message { get; set; }
        public List<InventoryViewModel> Inventory;
        public InventorySearchModel SearchModel;
        public SelectList Products;
        private readonly IProductApplication productApplication;
        private readonly IInventoryApplication InventoryApplication;
        public IndexModel(IProductApplication productApplication,
            IInventoryApplication InventoryApplication)
        {
            this.InventoryApplication = InventoryApplication;
            this.productApplication = productApplication;
        }
        public void OnGet(InventorySearchModel searchModel)
        {
            Products = new SelectList(productApplication.GetProducts(), "Id", "Name");//مقدار اسم را از لیست بگیر ونمایش بده و مقدار آیدی هم داخل آیدی قرارا بده
            Inventory = InventoryApplication.Search(searchModel);
        }
        public IActionResult OnGetCreate()
        {
            var command = new CreateInventory
            {
                Products = (productApplication.GetProducts())
            };
            return Partial("./Create", command);
        }
        public JsonResult OnPostCreate(CreateInventory command)
        {
            var result = InventoryApplication.Create(command);
            return new JsonResult(result);
        }
        public IActionResult OnGetEdit(long id)
        {
            var inventory = InventoryApplication
                .GetDetails(id);
            inventory.Products = productApplication
                .GetProducts();
            return Partial("./Edit", inventory);
        }
        public JsonResult OnPostEdit(EditInventory command)
        {
            var result = InventoryApplication.Edit(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetIncrease(long id)
        {
            var command = new IncreaseInventory()
            {
                InventoryId = id
            };
            return Partial("./Increase", command);
        }
        public JsonResult OnPostIncrease(IncreaseInventory command)
        {
            var result = InventoryApplication.Increase(command);
            return new JsonResult(result);
        }
        public IActionResult OnGetReduce(long id)
        {
            var command = new ReduceInventory()
            {
                InventoryId = id
            };
            return Partial("./Reduce", command);
        }
        public JsonResult OnPostReduce(ReduceInventory command)
        {
            var result = InventoryApplication.Reduce(command);
            return new JsonResult(result);
        }
        public IActionResult OnGetLog(long id)
        {
            var log = InventoryApplication.GetOperationLog(id);
            return Partial("OperationLog", log);
        }
    }
}
