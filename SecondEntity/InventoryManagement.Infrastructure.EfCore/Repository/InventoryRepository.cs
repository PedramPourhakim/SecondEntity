﻿using _0_Framework.Infrastructure;
using InventoryManagement.Application.Contracts.Inventory;
using InventoryManagement.Domain.InventoryAgg;
using ShopManagement.Infrastructure.EfCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Infrastructure.EfCore.Repository
{
    public class InventoryRepository : RepositoryBase<long, Inventory>, IInventoryRepository
    {
        private readonly InventoryContext context;
        private readonly ShopContext shopContext;
        public InventoryRepository(InventoryContext context, ShopContext shopContext) : base(context)
        {
            this.context = context;
            this.shopContext = shopContext;
        }

        public Inventory GetBy(long productId)
        {
            return context.Inventory
                .FirstOrDefault(x => x.ProductId == productId);
        }

        public EditInventory GetDetails(long id)
        {
            return context.Inventory.Select(x =>
            new EditInventory
            {
                Id = x.Id,
                ProductId = x.ProductId,
                UnitPrice = x.UnitPrice
            }).FirstOrDefault(x => x.Id == id);
        }

        public List<InventoryViewModel> Search
            (InventorySearchModel searchModel)
        {
            var products = shopContext
                .Products.Select(x => new
                {
                    x.Id,
                    x.Name
                }).ToList();
            var query = context.Inventory.Select(x
                => new InventoryViewModel
                {
                    Id = x.Id,
                    UnitPrice = x.UnitPrice,
                    InStock = x.InStock,
                    ProductId = x.ProductId,
                    CurrentCount = x.CalculateCurrentCount()
                });
            if (searchModel.ProductId > 0)
                query = query.Where(x => x.ProductId
                  == searchModel.ProductId);
            if (searchModel.InStock == false)
                query = query.Where(x => x.InStock
                == false);
            var inventory = query.
                OrderByDescending(x => x.Id).ToList();

            inventory.ForEach(item => 
            {
                item.Product = products
                .FirstOrDefault(x => x.Id == item.ProductId)
                ?.Name;
            });

            return inventory;
        }
    }
}