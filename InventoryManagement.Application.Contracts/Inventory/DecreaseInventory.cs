
namespace InventoryManagement.Application.Contracts.Inventory
{
    public class DecreaseInventory
    {
        public long InventoryId { get; set; }
        public string Description { get; set; }
        public long OperatorId { get; set; }
        public int Count { get; set; }
        public long OrderId { get; set; }
        public long ProductId { get; set; }
    }


}
