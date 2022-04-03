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
using ShopManagement.Application.Contracts.SlideContracts;

namespace ServiceHost.Areas.Administration.Pages.Shop.Slides
{
    public class IndexModel : PageModel
    {
        [TempData]
        public string Message { get; set; }
        public List<SlideViewModel> Slides;
        private readonly ISlideApplication slideApplication;

        public IndexModel(ISlideApplication slideApplication)
        {
            this.slideApplication = slideApplication;
        }

        public void OnGet()
        {
            Slides = slideApplication.GetList();
        }
        public IActionResult OnGetCreate()
        {
            var command = new CreateSlide();
            return Partial("./Create",command);
        }
        public JsonResult OnPostCreate(CreateSlide command)
        {
            var result = slideApplication.Create(command);
            return new JsonResult(result);
        }
        public IActionResult OnGetEdit(long id)
        {
            var slide = slideApplication.GetDetails(id);
            return Partial("./Edit", slide);
        }
        public JsonResult OnPostEdit(EditSlide command)
        {
            var result = slideApplication.Edit(command);
            return new JsonResult(result);
        }
        public IActionResult OnGetRemove(long id)
        {
            var result=slideApplication.Remove(id);
            if (result.Movafagh==true)
            {
                return RedirectToPage("./Index");
            }
            Message = result.Message;
            return RedirectToPage("./Index");
        }
        public IActionResult OnGetRestore(long id)
        {            
            var result = slideApplication.Restore(id);
            if (result.Movafagh == true)
            {
                return RedirectToPage("./Index");
            }
            Message = result.Message;
            return RedirectToPage("./Index");
        }
    }
}
