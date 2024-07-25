
namespace InventoryManagement.Application.Contracts.Inventory
{
    public class IncreaseInventory
    {
        public long InventoryId { get; set; }
        public string Description { get; set; }
        public long OperatorId { get; set; }
        public int Count { get; set; }
    }


}
