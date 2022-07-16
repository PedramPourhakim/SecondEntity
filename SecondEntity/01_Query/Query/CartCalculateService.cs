using _0_Framework.Application;
using _0_Framework.Infrastructure;
using _01_Query.Contract;
using DiscountManagement.Infrastructure.EfCore;
using ShopManagement.Application.Contracts.Order;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _01_Query.Query
{
    public class CartCalculateService : ICartCalculatorService
    {
        private readonly IAuthHelper authHelper;
        private readonly DiscountContext discountContext;
        public CartCalculateService(DiscountContext discountContext)
        {
            this.discountContext = discountContext;
        }
        public Cart ComputeCart(List<CartItem> cartItems)
        {
            var cart = new Cart();
            var currentAccountRole = authHelper.CurrentAccountRole();
            var colleagueDiscounts = discountContext.
                ColleagueDiscounts.Where(x => x.IsRemoved == false)
                .Select(x => new
                {
                    x.DiscountRate,
                    x.ProductId,
                    x.IsRemoved
                }).ToList();

            var customerDiscounts = discountContext.
                CustomerDiscounts.Where(x => x.StartDate < DateTime.Now
                && x.EndDate > DateTime.Now).Select(x => new
                { x.DiscountRate, x.ProductId, }).ToList();

         
                foreach (var cartItem in cartItems)
            {
                //var discount = new DiscountHelper();
                if (currentAccountRole == Roles.
                    ColleagueUser)
                {
                    var colleagueDiscount = 
                        colleagueDiscounts
                        .FirstOrDefault(x => x.ProductId
                        == cartItem.Id);
                    if (colleagueDiscount != null)
                    {
                      cartItem.DiscountRate = colleagueDiscount
                            .DiscountRate;
                    }

                }
                else
                {
                    var customerDiscount = customerDiscounts.
                        FirstOrDefault(x => x.ProductId ==
                        cartItem.Id);
                    if (customerDiscount != null)
                    {
                        cartItem.DiscountRate = customerDiscount.
                            DiscountRate;
                    }
                }
                cartItem.DiscountAmount = ((cartItem.TotalItemPrice
                     * cartItem.DiscountRate) / 100);
                cartItem.ItemPayAmount = cartItem.TotalItemPrice
                    - cartItem.DiscountAmount;
                cart.Add(cartItem);
            }
           
            return cart;
        }
    }
}
