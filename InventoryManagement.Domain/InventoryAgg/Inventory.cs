using Framework.Domain;
using System.Collections.Generic;
using System.Linq;

namespace InventoryManagement.Domain.InventoryAgg
{
    public class Inventory : EntityBase
    {
        public long ProductId { get; private set; }
        public bool IsInStock { get { return CurrentCount > 0; } }
        public int CurrentCount { get { return CalculateCurrentCount(); } }
        public decimal UnitPrice { get; private set; }
        public List<InventoryOperation> InventoryOperations { get; private set; }

        public Inventory(long productId, decimal unitPrice)
        {
            ProductId = productId;
            UnitPrice = unitPrice;
        }

        public void Edit(long productId, decimal unitPrice)
        {
            ProductId = productId;
            UnitPrice = unitPrice;
        }

        public int CalculateCurrentCount()
        {
            int positive = InventoryOperations.Where(x => x.IsSold == false).Sum(x => x.Count);
            int negative = InventoryOperations.Where(x => x.IsSold == true).Sum(x => x.Count);
            return positive - negative;

        }
        public void Increase(int count, long operatorId, string description)
        {
            var operation = new InventoryOperation(false, operatorId, count, CurrentCount, 0, description, Id);
            InventoryOperations.Add(operation);
        }
        public void Decrease(int count, long orderId, long operatorId, string description)
        {
            var operation = new InventoryOperation(true, operatorId, count, CurrentCount, orderId, description, Id);
            InventoryOperations.Add(operation);
        }

    }
}
