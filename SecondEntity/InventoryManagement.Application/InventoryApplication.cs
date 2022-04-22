using _0_Framework.Application;
using InventoryManagement.Application.Contracts.Inventory;
using InventoryManagement.Domain.InventoryAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Application
{
    public class InventoryApplication : IInventoryApplication
    {
        private readonly IInventoryRepository inventoryRepository;

        public InventoryApplication(IInventoryRepository inventoryRepository)
        {
            this.inventoryRepository = inventoryRepository;
        }

        public OperationResult Create(CreateInventory command)
        {
            var operation = new OperationResult();
            if (inventoryRepository
                .Exists(x => x.ProductId ==
                command.ProductId))
                return operation.Failed
                    (ApplicationMessages.DuplicatedRecord);
            var inventory = new Inventory
                (command.ProductId, command.UnitPrice);
            inventoryRepository.Create(inventory);
            inventoryRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Edit(EditInventory command)
        {
            var operation = new OperationResult();
            var inventory = inventoryRepository
                .Get(command.Id);
            if (inventory == null)
                return operation.Failed(ApplicationMessages
                    .RecordNotFound);

            if (inventoryRepository.Exists(x =>
             x.ProductId == command.ProductId && x.Id 
             != command.Id))
                return operation.
                    Failed(ApplicationMessages.
                    DuplicatedRecord);

            inventory.Edit(command.ProductId, command.UnitPrice);
            inventoryRepository.SaveChanges();
            return operation.Succeeded();
        }

        public EditInventory GetDetails(long id)
        {
            return inventoryRepository.GetDetails(id);
        }

        public List<InventoryOperationViewModel> GetOperationLog(long InventoryId)
        {
            return inventoryRepository.GetOperationLog(InventoryId);
        }

        public OperationResult Increase(IncreaseInventory command)
        {
            var operation = new OperationResult();
            var inventory = inventoryRepository
                .Get(command.InventoryId);
            if (inventory == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            const long OperatorId = 1;
            inventory.Increase(command.Count,
                OperatorId, command.Description);
            inventoryRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Reduce(ReduceInventory command)
        {
            var operation = new OperationResult();
            var inventory = inventoryRepository
                .Get(command.InventoryId);
            if (inventory == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            const long OperatorId = 1;
            inventory.Reduce(command.Count,
                OperatorId, command.Description,0);
            inventoryRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Reduce(List<ReduceInventory> command)
        {
            var operation = new OperationResult();
            foreach (var item in command)
            {
                var inventory =
                    inventoryRepository.GetBy(item.ProductId);
                const long OperatorId = 1;
                inventory.Reduce(item.Count, OperatorId
                    , item.Description, item.OrderId);
            }
            inventoryRepository.SaveChanges();
            return operation.Succeeded();
        }

        public List<InventoryViewModel> Search(InventorySearchModel searchModel)
        {
            return inventoryRepository.Search(searchModel);
        }
    }
}
