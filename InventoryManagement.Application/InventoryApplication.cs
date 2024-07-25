using Framework.Application;
using InventoryManagement.Application.Contracts.Inventory;
using InventoryManagement.Domain.InventoryAgg;
using System.Collections.Generic;
using System.Linq;

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
            OperationResult operation = new();
            Inventory entity = new(command.ProductId, command.UnitPrice);
            if (inventoryRepository.Exists(x => x.ProductId == command.ProductId))
                return operation.Failed(ApplicationMessages.DuplicatedMessage);
            inventoryRepository.Create(entity);
            return operation.Succeeded();
        }

        public OperationResult Edit(EditInventory command)
        {
            OperationResult operation = new();
            Inventory data = inventoryRepository.Get(command.Id);
            if (data == null)
                return operation.Failed(ApplicationMessages.NotFoundMessage);
            data.Edit(command.ProductId, command.UnitPrice);
            inventoryRepository.Update(data);
            return operation.Succeeded();

        }

        public EditInventory EditGet(long id)
        {
            var data = inventoryRepository.Get(id);
            if (data == null)
                return null;
            return new EditInventory() { UnitPrice = data.UnitPrice, ProductId = data.ProductId, Id = id };
        }

        public OperationResult Increase(IncreaseInventory command)
        {
            var data = inventoryRepository.Get(command.InventoryId);
            OperationResult operation = new();
            if (data == null)
                return operation.Failed(ApplicationMessages.NotFoundMessage);
            data.Increase(command.Count, command.OperatorId, command.Description);
            inventoryRepository.Update(data);
            return operation.Succeeded();
        }
        public OperationResult Decrease(DecreaseInventory command)
        {
            var data = inventoryRepository.Get(command.InventoryId);
            OperationResult operation = new();
            if (data == null)
                return operation.Failed(ApplicationMessages.NotFoundMessage);
            data.Decrease(command.Count, command.OrderId, command.OperatorId, command.Description);
            inventoryRepository.Update(data);
            return operation.Succeeded();
        }
        public OperationResult Decrease(List<DecreaseInventory> command)
        {
            OperationResult operation = new();

            foreach (var item in command)
            {
                if (!inventoryRepository.Exists(x => x.ProductId == item.ProductId))
                    return operation.Failed(ApplicationMessages.NotFoundMessage);
                Decrease(item);
            }
            return operation.Succeeded();
        }

        public OperationResult Delete(long id)
        {
            OperationResult op = new();
            var product = inventoryRepository.Get(id);
            if (product == null)
                return op.Failed(ApplicationMessages.NotFoundMessage);
            else
            {
                inventoryRepository.Delete(product);
                return op.Succeeded();
            }
        }
        public List<InventoryViewModel> Search(InventorySearchModel command)
        {
            return inventoryRepository.Search(command).ToList();
        }

        public InventoryViewModel GetInventory(long id)
        {
            return inventoryRepository.GetInventoryWithProduct(id);
        }

        public List<InventoryOperationViewModel> GetInventoryLog(long inventoryId)
        {
            return inventoryRepository.Get(inventoryId).InventoryOperations.Select(x => new InventoryOperationViewModel
            {
                Count = x.Count,
                CountBeforeOperation = x.CountBeforeOperation,
                Description = x.Description,
                Id = x.Id,
                IsSold = x.IsSold,
                OperationDateTime = x.OperationDateTime,
                OperatorId = x.OperatorId,
                OperatorName = "مدیر",
                OrderId = x.OrderId

            }).ToList();
        }

        public List<InventoryOperationViewModel> GetInventoryLogs()
        {
            return inventoryRepository.GetInventoryLogs();
        }
    }
}