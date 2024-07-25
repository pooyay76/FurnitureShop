
namespace InventoryManagement.Application.Contracts.Inventory
{
    public class InventoryViewModel
    {
        public long Id{ get; set; }
        public string ProductName { get; set; }
        public long ProductId { get; set; }
        public decimal UnitPrice { get; set; }
        public int Count { get; set; }
        public bool IsInStock { get; set; }

    }


}
