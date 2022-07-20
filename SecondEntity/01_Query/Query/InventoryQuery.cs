using _01_Query.Contract.Inventory;
using InventoryManagement.Infrastructure.EfCore;
using ShopManagement.Infrastructure.EfCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_Query.Query
{
    public class InventoryQuery : IInventoryQuery
    {
        private readonly InventoryContext inventoryContext;
        private readonly ShopContext shopContext;
        public InventoryQuery(InventoryContext inventoryContext,
            ShopContext shopContext)
        {
            this.inventoryContext = inventoryContext;
            this.shopContext = shopContext;
        }
        public StockStatus CheckStock(IsInStock command)
        {
            var inventory = inventoryContext.Inventory
                .FirstOrDefault(x => x.ProductId ==
                command.ProductId);

            if(inventory==null || 
                inventory.CalculateCurrentCount()< command.Count)
            {
                var product = shopContext.Products
                    .Select(x => new { x.Id,x.Name})
                    .FirstOrDefault(x => x.Id ==
                    command.ProductId);
                return new StockStatus
                {
                    IsStock = false,
                    ProductName = product?.Name
                };
            }
            return new StockStatus
            {
                IsStock = true
            };


        }
    }
}
