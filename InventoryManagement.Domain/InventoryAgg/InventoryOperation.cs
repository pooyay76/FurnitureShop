using System;

namespace InventoryManagement.Domain.InventoryAgg
{
    public class InventoryOperation
    {
        public InventoryOperation(bool isSold, long operatorId, int count,int countBeforeOperation, long orderId, string description, long inventoryId)
        {
            IsSold = isSold;
            OperatorId = operatorId;
            Count = count;
            OrderId = orderId;
            Description = description;
            InventoryId = inventoryId;
            CountBeforeOperation = countBeforeOperation;
            OperationDateTime = DateTime.Now;
        }

        public long Id { get; private set; }
        public bool IsSold { get; private set; }
        public long OperatorId { get; private set; }
        public int CountBeforeOperation { get; private set; }
        public int Count { get; private set; }
        public long OrderId { get; private set; }
        public string Description { get; private set; }
        public long InventoryId { get; private set; }
        public Inventory Inventory { get; private set; }
        public DateTime OperationDateTime { get; private set; }

    }
}
