using InventoryManagement.Infrastructure.EfCore;
using ShopManagement.Application.Contracts.Order;
using ShopManagement.Infrastructure.EfCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_Query.Contract
{
    public interface ICartCalculatorService
    {
        Cart ComputeCart(List<CartItem> cartItems);
    }
}
