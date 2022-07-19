using InventoryManagement.Application.Contracts.Inventory;
using ShopManagement.Domain.OrderAgg;
using ShopManagement.Domain.Services;
using System;
using System.Collections.Generic;

namespace ShopManagement.Infrastructure.InventoryAcl
{
    public class ShopInventoryAcl : IShopInventoryAcl
    {
        private readonly IInventoryApplication inventoryApplication;
        public ShopInventoryAcl(IInventoryApplication inventoryApplication)
        {
            this.inventoryApplication = inventoryApplication;
        }
        public bool ReduceFromInventory(List<OrderItem> items)
        {
            var command = new List<ReduceInventory>();
            foreach (var OrderItem in items)
            {
                var item = new ReduceInventory(
                    OrderItem.ProductId,OrderItem.Count,
                    "خرید مشتری",OrderItem.OrderId);
                command.Add(item);
            }
            return inventoryApplication.Reduce(command).Movafagh;
        }
    }
}
