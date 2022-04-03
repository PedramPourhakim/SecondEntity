using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.ProductCategoryContracts;
using ShopManagement.Application.Contracts.ProductContracts;
using ShopManagement.Application.Contracts.ProductPictureContracts;

namespace ServiceHost.Areas.Administration.Pages.Shop.ProductPictures
{
    public class IndexModel : PageModel
    {
        [TempData]
        public string Message { get; set; }
        public List<ProductPictureViewModel> ProductPictures;
        public ProductPictureSearchModel SearchModel;
        public SelectList Products;
        private readonly IProductApplication productApplication;
        private readonly IProductPictureApplication productPictureApplication;
            public IndexModel(IProductApplication productApplication,
                IProductPictureApplication productPictureApplication)
        {
            this.productApplication = productApplication;
            this.productPictureApplication = productPictureApplication;
        }
        public void OnGet(ProductPictureSearchModel searchModel)
        {
            Products = new SelectList(productApplication.GetProducts(),"Id","Name");//مقدار اسم را از لیست بگیر ونمایش بده و مقدار آیدی هم داخل آیدی قرارا بده
            ProductPictures= productPictureApplication.Search(searchModel);
        }
        public IActionResult OnGetCreate()
        {
            var command = new CreateProductPicture
            {
                Products=productApplication.GetProducts()
            };
            return Partial("./Create",command);
        }
        public JsonResult OnPostCreate(CreateProductPicture command)
        {
            var result = productPictureApplication.Create(command);
            return new JsonResult(result);
        }
        public IActionResult OnGetEdit(long id)
        {
            var productPicture = productPictureApplication.GetDetails(id);
            productPicture.Products = productApplication.GetProducts();
            return Partial("./Edit", productPicture);
        }
        public JsonResult OnPostEdit(EditProductPicture command)
        {
            var result = productPictureApplication.Edit(command);
            return new JsonResult(result);
        }
        public IActionResult OnGetRemove(long id)
        {
            var result=productPictureApplication.Remove(id);
            if (result.Movafagh==true)
            {
                return RedirectToPage("./Index");
            }
            Message = result.Message;
            return RedirectToPage("./Index");
        }
        public IActionResult OnGetRestore(long id)
        {            
            var result = productPictureApplication.Restore(id);
            if (result.Movafagh == true)
            {
                return RedirectToPage("./Index");
            }
            Message = result.Message;
            return RedirectToPage("./Index");
        }
    }
}
