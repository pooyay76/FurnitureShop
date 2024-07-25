using Framework.Application;
using System.Collections.Generic;

namespace InventoryManagement.Application.Contracts.Inventory
{
    public interface IInventoryApplication
    {
        OperationResult Create(CreateInventory command);
        OperationResult Edit(EditInventory command);
        OperationResult Increase(IncreaseInventory command);
        OperationResult Decrease(DecreaseInventory command);
        OperationResult Decrease(List<DecreaseInventory> command);
        EditInventory EditGet(long id);
        List<InventoryViewModel> Search(InventorySearchModel command);
        InventoryViewModel GetInventory(long id);
        List<InventoryOperationViewModel> GetInventoryLog(long inventoryId);
        public OperationResult Delete(long id);
        List<InventoryOperationViewModel> GetInventoryLogs();

    }
}
