using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _01_Query.Contract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Nancy.Json;
using ShopManagement.Application.Contracts.Order;

namespace ServiceHost.Pages
{
    public class CheckoutModel : PageModel
    {
        public Cart Cart;
        public List<CartItem> CartItems;
        public const string CookieName = "cart-items";

        private readonly ICartCalculatorService cartCalculatorService;

        public CheckoutModel(ICartCalculatorService cartCalculatorService)
        {
            this.cartCalculatorService = cartCalculatorService;
        }

        public void OnGet()
        {
            var serializer = new JavaScriptSerializer();
            var value = Request.Cookies[CookieName];
            var cartItems = serializer.Deserialize
                <List<CartItem>>(value);
            foreach (var item in cartItems)
                item.CalculateTotalItemPrice();
            Cart = cartCalculatorService.ComputeCart(cartItems);
        }
    }
}
