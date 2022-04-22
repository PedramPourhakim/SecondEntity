using _0_Framework.Domain;
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
        public long CalculateCurrentCount()
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
}
