using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using _0_Framework.Infrastructure;
using ShopManagement.Application.Contracts.Order;
using Microsoft.AspNetCore.Mvc.Rendering;
using AccountManagement.Application.Contracts.Account;

namespace ServiceHost.Areas.Administration.Pages.Shop.Orders
{
    //[Authorize(Roles = "1, 3")]
    public class IndexModel : PageModel
    {
        public OrderSearchModel SearchModel;
        public List<OrderViewModel> Orders;
        public SelectList Accounts;

        private readonly IOrderApplication orderApplication;
        private readonly IAccountApplication accountApplication;
        public IndexModel(IOrderApplication orderApplication,
            IAccountApplication accountApplication)
        {
            this.orderApplication = orderApplication;
            this.accountApplication = accountApplication;
        }

        public void OnGet(OrderSearchModel searchModel)
        {
            Accounts = new SelectList(accountApplication.GetAccounts(),"Id","FullName");
            Orders = orderApplication.Search(searchModel);
        }
        public IActionResult OnGetConfirm(long id)
        {
            orderApplication.PaymentSucceeded(id, 0);
            return RedirectToPage("./Index");
        }
        public IActionResult OnGetCancel(long id)
        {
            orderApplication.Cancel(id);
            return RedirectToPage("./Index");
        }
        public IActionResult OnGetItems(long id)
        {
            var items = orderApplication.GetItems(id);
            return Partial("Items", items);
        }
    }
}