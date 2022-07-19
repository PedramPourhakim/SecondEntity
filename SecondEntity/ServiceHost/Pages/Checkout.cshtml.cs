using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using _0_Framework.Application;
using _0_Framework.Application.ZarinPal;
using _01_Query.Contract;
using _01_Query.Contract.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Nancy.Json;
using ShopManagement.Application.Contracts.Order;

namespace ServiceHost.Pages
{
    [Authorize]
    public class CheckoutModel : PageModel
    {
        public Cart Cart;
        public List<CartItem> CartItems;
        public const string CookieName = "cart-items";

        private readonly IProductQuery productQuery;
        private readonly ICartCalculatorService cartCalculatorService;
        private readonly ICartService cartService;
        private readonly IOrderApplication orderApplication;
        private readonly IZarinPalFactory zarinPalFactory;
        private readonly IAuthHelper authHelper;

        public CheckoutModel(ICartCalculatorService 
            cartCalculatorService,ICartService 
            cartService,IOrderApplication orderApplication
            , IZarinPalFactory zarinPalFactory,
            IAuthHelper authHelper,IProductQuery productQuery)
        {
            this.cartCalculatorService = cartCalculatorService;
            this.cartService = cartService;
            this.orderApplication = orderApplication;
            this.zarinPalFactory = zarinPalFactory;
            this.authHelper = authHelper;
            this.productQuery = productQuery;
            Cart = new Cart();
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
        public IActionResult OnPostPay(int PaymentMethod)
        {
            var cart = cartService.Get();
            cart.SetPaymentMethod(PaymentMethod);

           var result= productQuery.CheckInventoryStatus
                (cart.Items);
            if (result.Any(x => !x.IsInStock))
                return RedirectToPage("/Cart");


            var orderId = orderApplication.PlaceOrder(cart);
            if (PaymentMethod == 1)
            {
                var paymentResponse = zarinPalFactory.CreatePaymentRequest(
                cart.PayAmount.ToString(CultureInfo.InvariantCulture),
                "", "", "خرید از درگاه لوازم خانگی و دکوری",
                orderId);
                return Redirect(
                  $"https://{zarinPalFactory.Prefix}.zarinpal.com/pg/StartPay/{paymentResponse.Authority}");
            }
            else
            {
                var paymentResult = new PaymentResult();
                return RedirectToPage("/PaymentResult",paymentResult.Succeeded("سفارش شما با موفقیت ثبت شد،پس از تماس کارشناسان ما و پرداخت وجه،سفارش ارسال خواهد شد.",null));
            }
            

           

        }
        public IActionResult OnGetCallBack([FromQuery]
        string authority, [FromQuery] string status,
             [FromQuery] long oId)
        {
            
            var orderAmount = orderApplication.GetAmountBy(oId);
            var verificationResponse =
            zarinPalFactory.CreateVerificationRequest
            (authority,orderAmount.ToString(CultureInfo.InvariantCulture));

            var result = new PaymentResult();
            if (status == "OK" && verificationResponse.Status >= 100)
            {
                var IssueTrackingNo = orderApplication.PaymentSucceeded(oId, verificationResponse.RefID);
                Response.Cookies.Delete("cart-items");
                result = result.Succeeded("پرداخت با موفقیت انجام شد.", IssueTrackingNo);
                return RedirectToPage("/PaymentResult", result);
            }

            result = result.Failed(
                "پرداخت با موفقیت انجام نشد. درصورت کسر وجه از حساب، مبلغ تا 24 ساعت دیگر به حساب شما بازگردانده خواهد شد.");
            return RedirectToPage("/PaymentResult", result);
        }
    }
}
