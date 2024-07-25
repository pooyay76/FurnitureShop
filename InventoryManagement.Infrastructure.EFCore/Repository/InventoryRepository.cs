using Framework.Infrastructure;
using InventoryManagement.Application.Contracts.Inventory;
using InventoryManagement.Domain.InventoryAgg;
using ShopManagement.Infrastructure.EfCore;
using System.Collections.Generic;
using System.Linq;

namespace InventoryManagement.Infrastructure.EFCore.Repository
{
    public class InventoryRepository : RepositoryBase<long, Inventory>, IInventoryRepository
    {
        private readonly InventoryContext inventoryContext;
        private readonly ShopContext shopContext;

        public InventoryRepository(InventoryContext inventoryContext, ShopContext shopContext) : base(inventoryContext)
        {
            this.inventoryContext = inventoryContext;
            this.shopContext = shopContext;
        }

        public IEnumerable<InventoryViewModel> Search(InventorySearchModel inventorySearchModel)
        {
            var query = inventoryContext.Inventories.AsQueryable();
            if (inventorySearchModel.ProductId != default)
                query = query.Where(x => x.ProductId == inventorySearchModel.ProductId);

            var products = shopContext.Products.Select(x => new { x.Id, x.Name });
            var result = from item in query.AsEnumerable()
                         join product in products.AsEnumerable() on item.ProductId equals product.Id
                         select new InventoryViewModel()
                         {
                             Id = item.Id,
                             ProductId = item.ProductId,
                             Count = item.CurrentCount,
                             IsInStock = item.IsInStock,
                             UnitPrice = item.UnitPrice,
                             ProductName = product.Name
                         };


            return result.OrderByDescending(x => x.Id);
        }
        public InventoryViewModel GetInventoryWithProduct(long id)
        {
            var data = inventoryContext.Inventories.FirstOrDefault(x => x.Id == id);
            var product = shopContext.Products.FirstOrDefault(x => x.Id == data.ProductId);
            if (data == null || product == null)
                return null;
            return new InventoryViewModel()
            {

                Count = data.CurrentCount,
                ProductId = data.ProductId,
                Id = data.Id,
                IsInStock = data.IsInStock,
                ProductName = product.Name,
                UnitPrice = data.UnitPrice
            };
        }

        public IEnumerable<InventoryOperationViewModel> GetInventoryLog(long invetoryId)
        {
            inventoryContext.Set<InventoryOperation>();
            Inventory data = base.Get(invetoryId);
            if (data == null)
                return null;
            return data.InventoryOperations.Select(x => new InventoryOperationViewModel
            {
                Count = data.CurrentCount,
                CountBeforeOperation = x.CountBeforeOperation,
                Description = x.Description,
                Id = x.Id,
                IsSold = x.IsSold,
                OperationDateTime = x.OperationDateTime,
                OperatorId = x.OperatorId,
                OperatorName = "مدیر",
                OrderId = x.OrderId
            });

        }
        public List<InventoryOperationViewModel> GetInventoryLogs()
        {
            var products = shopContext.Products.Select(x => new { ProductId = x.Id, ProductName = x.Name })?.ToList();
            var inventories = inventoryContext.Inventories.AsEnumerable();
            if (inventories == null) return null;

            List<InventoryOperationViewModel> result = inventories.SelectMany(x => x.InventoryOperations.Select(y => new InventoryOperationViewModel
            {
                Count = y.Count,
                CountBeforeOperation = y.CountBeforeOperation,
                Description = y.Description,
                Id = y.Id,
                IsSold = y.IsSold,
                OperationDateTime = y.OperationDateTime,
                OperatorId = y.OperatorId,
                OperatorName = "مدیر",
                OrderId = y.OrderId,
                ProductId = x.ProductId,
                ProductName = products.FirstOrDefault(y => y.ProductId == x.ProductId)?.ProductName ?? "کالای حذف شده"
            })).ToList();

            return result.OrderByDescending(x => x.Id).ToList();
        }
    }
}
