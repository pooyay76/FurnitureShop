using System.ComponentModel.DataAnnotations;

namespace DiscountManagement.Application.Contracts.ColleagueDiscount
{
    public class ColleagueDiscountViewModel
    {
        public string Name { get; set; }
        public long Id { get; set; }
        public long ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal DiscountPercentage { get; set; }
        public string CreationDate { get; set; }
    }

}