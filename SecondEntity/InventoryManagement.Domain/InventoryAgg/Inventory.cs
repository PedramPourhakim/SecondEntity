using _0_Framework.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InventoryManagement.Domain.InventoryAgg
{
    public class Inventory:EntityBase
    {
        public long ProductId { get; private set; }
        public double UnitPrice { get; private set; }
        public bool InStock { get; private set; }
        public List<InventoryOperation> Operations { get; private set; }

        public Inventory(long productId, double unitPrice)
        {
            ProductId = productId;
            UnitPrice = unitPrice;
            InStock = false;
        }
        private long CalculateCurrentCount()
        {
            var plus = Operations.Where(x => x.Operation
              ==true).Sum(x=>x.Count);
            var minus = Operations.Where(x => x.Operation
              ==false).Sum(x=>x.Count);
            return plus - minus;
        }
        public void Increase(long count,long operatorId,
            string description)
        {
            var CurrentCount = CalculateCurrentCount()+count;
            var operation = new InventoryOperation(true, count,
                operatorId, CurrentCount, description, 0, Id);
            Operations.Add(operation);
            //if (CurrentCount > 0)
            //    InStock = true;
            //else
            //    InStock = false;
            InStock = CurrentCount > 0;
        }
        public void Reduce(long count, long operatorId,
            string description,long orderId)
        {
            var CurrentCount = CalculateCurrentCount()
                - count;
            var operation = new InventoryOperation(false, count,
                operatorId, CurrentCount, description, orderId
                , Id);
            Operations.Add(operation);
            InStock = CurrentCount > 0;
        }
    }
    public class InventoryOperation
    {
        public long Id { get; private set; }
        public bool Operation { get; private set; }
        public long Count { get; private set; }
        public long OperatorId { get; private set; }
        public DateTime OperationDate { get; private set; }
        public long CurrentCount { get; private set; }
        public string Description { get; private set; }
        public long OrderId { get; private set; }
        public long InventoryId { get; private set; }
        public Inventory Inventory { get; private set; }

        public InventoryOperation(bool operation,
            long count, long operatorId, 
            long currentCount, string description,
            long orderId, long inventoryId)
        {
            Operation = operation;
            Count = count;
            OperatorId = operatorId;
            CurrentCount = currentCount;
            Description = description;
            OrderId = orderId;
            InventoryId = inventoryId;
        }
    }
}
