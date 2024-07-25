using Framework.Application;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace DiscountManagement.Application.Contracts.CustomerDiscount
{
    public class DefineCustomerDiscount
    {
        [Required(ErrorMessage = ValidationMessages.RequiredMessage, AllowEmptyStrings = false)]
        [Range(1, 100000, ErrorMessage = ValidationMessages.InvalidModelStateMessage)]
        public long ProductId { get; set; }
        [Required(ErrorMessage = ValidationMessages.RequiredMessage, AllowEmptyStrings = false)]
        public string Reason { get; set; }
        [Required(ErrorMessage = ValidationMessages.RequiredMessage, AllowEmptyStrings = false)]
        public string StartDate { get; set; }
        [Required(ErrorMessage = ValidationMessages.RequiredMessage, AllowEmptyStrings = false)]
        public string EndDate { get; set; }
        [Required(ErrorMessage = ValidationMessages.RequiredMessage, AllowEmptyStrings = false)]
        [Range(1, 99)]
        public int DiscountPercentage { get; set; }
        public SelectList Products { get; set; }
    }

}
