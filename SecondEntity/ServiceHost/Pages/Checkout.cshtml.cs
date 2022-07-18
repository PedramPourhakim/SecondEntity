using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _0_Framework.Application;
using _0_Framework.Application.ZarinPal;
using _01_Query.Contract;
using _01_Query.Contract.Product;
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
        private readonly ICartService cartService;
        private readonly IProductQuery productQuery;
        private readonly IOrderApplication orderApplication;
        private readonly IZarinPalFactory zarinPalFactory;
        private readonly IAuthHelper authHelper;

        public CheckoutModel(ICartCalculatorService 
            cartCalculatorService,ICartService 
            cartService,IOrderApplication orderApplication
            , IZarinPalFactory zarinPalFactory,
            IAuthHelper authHelper)
        {
            this.cartCalculatorService = cartCalculatorService;
            this.cartService = cartService;
            this.orderApplication = orderApplication;
            this.zarinPalFactory = zarinPalFactory;
            this.authHelper = authHelper;
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
            cartService.Set(Cart);
        }
        public IActionResult OnGetPay()
        {
            var cart = cartService.Get();

           var result= productQuery.CheckInventoryStatus
                (cart.Items);
            if (result.Any(x => !x.IsInStock))
                return RedirectToPage("/Cart");


            var orderId = orderApplication.PlaceOrder(cart);

            var accountUserName = authHelper.CurrentAccountInfo().Username;

            var paymentResponse=zarinPalFactory.CreatePaymentRequest(
                cart.PayAmount.ToString(),
                "", accountUserName, "خرید از درگاه لوازم خانگی و دکوری",
                orderId);

            return RedirectToPage("/Checkout");

        }
        public IActionResult OnGetCallBack([FromQuery] string authority, [FromQuery] string status,
             [FromQuery] long oId)
        {
            return null;
            //var orderAmount = orderApplication.GetAmountBy(oId);
            //var verificationResponse =
            //    //zarinPalFactory.CreateVerificationRequest(authority,
            //    //    orderAmount.ToString(CultureInfo.InvariantCulture));

            //var result = new PaymentResult();
            //if (status == "OK" && verificationResponse.Status >= 100)
            //{
            //    //var issueTrackingNo = orderApplication.PaymentSucceeded(oId, verificationResponse.RefID);
            //    Response.Cookies.Delete("cart-items");
            //    result = result.Succeeded("پرداخت با موفقیت انجام شد.", issueTrackingNo);
            //    return RedirectToPage("/PaymentResult", result);
            //}

            //result = result.Failed(
            //    "پرداخت با موفقیت انجام نشد. درصورت کسر وجه از حساب، مبلغ تا 24 ساعت دیگر به حساب شما بازگردانده خواهد شد.");
            //return RedirectToPage("/PaymentResult", result);
        }
    }
}
