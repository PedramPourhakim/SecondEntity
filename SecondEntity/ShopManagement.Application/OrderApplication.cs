using _0_Framework.Application;
using Microsoft.Extensions.Configuration;
using ShopManagement.Application.Contracts.Order;
using ShopManagement.Domain.OrderAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagement.Application
{
    public class OrderApplication : IOrderApplication
    {
        private readonly IOrderRepository orderRepository;
        private readonly IAuthHelper authHelper;
        private readonly IConfiguration configuration;
        public OrderApplication(IOrderRepository orderRepository,IAuthHelper authHelper,IConfiguration configuration)
        {
            this.orderRepository = orderRepository;
            this.authHelper = authHelper;
            this.configuration = configuration;
        }

        public void PaymentSucceeded(long OrderId,long refId)
        {
            var order = orderRepository.Get(OrderId);
            order.PaymentSucceeded(refId);
            var symbol = configuration.GetValue<string>("Symbol");
            var issueTrackingNo = CodeGenerator.Generate(symbol);
            order.SetIssueTrackingNo(issueTrackingNo);
            //Reduce Order Items From Inventory

            orderRepository.SaveChanges();
        }

        public long PlaceOrder(Cart cart)
        {
            var cuurentAccountId = authHelper.CurrentAccountId();

            var order = new Order(cuurentAccountId,
                cart.TotalAmount,cart.DiscountAmount,
                cart.PayAmount);

            foreach (var cartItem in cart.Items)
            {
                var orderItem = new OrderItem(cartItem.Id,
                    cartItem.Count,cartItem.UnitPrice,
                    cartItem.DiscountRate);
                order.AddItem(orderItem);
            }
            orderRepository.Create(order);
            orderRepository.SaveChanges();
            return order.Id;
        }
    }
}
