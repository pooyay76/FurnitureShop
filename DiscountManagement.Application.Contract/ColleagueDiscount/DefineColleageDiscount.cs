using Framework.Application;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.ComponentModel.DataAnnotations;

namespace DiscountManagement.Application.Contracts.ColleagueDiscount
{
    public class DefineColleagueDiscount
    {
        [Required(ErrorMessage = ValidationMessages.RequiredMessage)]
        [Display(Name = "نام تخفیف")]
        public string Name { get; set; }

        [Required(ErrorMessage = ValidationMessages.RequiredMessage)]
        [Display(Name = "محصول مورد نظر")]
        [Range(1, long.MaxValue)]
        public long ProductId { get; set; }

        [Required(ErrorMessage = ValidationMessages.RequiredMessage)]
        [Display(Name = "درصد تخفیف")]
        [Range(0, 99.99)]
        public decimal DiscountPercentage { get; set; }

        public SelectList Products { get; set; }
    }

}