using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagement.Application.Contracts.Order
{
    public interface IOrderApplication
    {
        long PlaceOrder(Cart cart);
        List<OrderItemViewModel> GetItems(long orderId);
        double GetAmountBy(long id);
        void Cancel(long id);
        string PaymentSucceeded(long OrderId,long refId);
        List<OrderViewModel> Search(OrderSearchModel searchModel);
    }
}
