using InventoryManagement.Application.Contracts.Inventory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace ServiceHost.Areas.Administration.Pages.Inventory
{
    public class InventoriesReportModel : PageModel
    {
        private readonly IInventoryApplication inventoryApplication;

        public InventoriesReportModel(IInventoryApplication inventoryApplication)
        {
            this.inventoryApplication = inventoryApplication;
        }

        public List<InventoryOperationViewModel> InventoryOperations { get; set; }
        public void OnGet()
        {
            InventoryOperations = inventoryApplication.GetInventoryLogs();
        }
    }
}
