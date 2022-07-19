using _0_Framework.Application;
using _0_Framework.Infrastructure;
using AccountManagement.Infrastructure.EfCore;
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
        private readonly AccountContext accountContext;
        private readonly InventoryContext context;
        private readonly ShopContext shopContext;
        public InventoryRepository(InventoryContext context
            , ShopContext shopContext,AccountContext accountContext) : base(context)
        {
            this.context = context;
            this.shopContext = shopContext;
            this.accountContext = accountContext;
        }

        public Inventory GetBy(long productId)
        {
            return context.Inventory
                .FirstOrDefault(x => x.ProductId
                == productId);
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

        public List<InventoryOperationViewModel> GetOperationLog(long InventoryId)
        {
            var accounts = accountContext.Accounts.Select
                (x => new
                {
                    x.Id,
                    x.FullName
                }).ToList();

            var inventory = context.Inventory.FirstOrDefault
                 (x => x.Id == InventoryId);

            var Operations= inventory.Operations.Select(x =>
            new InventoryOperationViewModel
            {
                Id=x.Id,
                Count=x.Count,
                CurrentCount=x.CurrentCount,
                Description=x.Description,
                Operation=x.Operation,
                OperationDate=x.OperationDate.ToFarsi(),
                OperatorId=x.OperatorId,
                OrderId=x.OrderId,
            }).OrderByDescending(x=>x.Id).ToList();
            foreach (var operation in Operations)
            {
                operation.Operator = accounts.FirstOrDefault
                    (x => x.Id == operation.OperatorId)?.FullName;
            }
            return Operations;
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
                    CurrentCount = x.CalculateCurrentCount(),
                    CreationDate=x.CreationDate.ToFarsi()
                });
            if (searchModel.ProductId > 0)
                query = query.Where(x => x.ProductId
                  == searchModel.ProductId);
            if (searchModel.InStock == true)
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
