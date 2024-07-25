using Framework.Application;
using Framework.Domain;
using InventoryManagement.Application.Contracts.Inventory;
using System.Collections.Generic;

namespace InventoryManagement.Domain.InventoryAgg
{
    public interface IInventoryRepository:IRepository<long,Inventory>
    {
        public IEnumerable<InventoryViewModel> Search(InventorySearchModel inventorySearchModel);
        public InventoryViewModel GetInventoryWithProduct(long inventoryId);
        public IEnumerable<InventoryOperationViewModel> GetInventoryLog(long invetoryId);
        public List<InventoryOperationViewModel> GetInventoryLogs();

    }
}
