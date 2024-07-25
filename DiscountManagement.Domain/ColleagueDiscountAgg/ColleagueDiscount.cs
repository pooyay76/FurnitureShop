using Framework.Domain;

namespace DiscountManagement.Domain.ColleagueDiscountAgg
{
    public class ColleagueDiscount : EntityBase
    {
        public string Name { get; private set; }
        public long ProductId { get; private set; }
        public decimal DiscountPercentage { get; private set; }
        public bool IsRemoved { get; private set; }

        public ColleagueDiscount(string name,long productId, decimal discountPercentage)
        {
            Name = name;
            ProductId = productId;
            DiscountPercentage = discountPercentage;
        }
        public void Edit(string name, long productId, decimal discountPercentage)
        {
            Name = name;
            ProductId = productId;
            DiscountPercentage = discountPercentage;
        }
        public void Remove()
        {
            IsRemoved = true;
        }
        public void Restore()
        {
            IsRemoved = false;
        }
    }
}
