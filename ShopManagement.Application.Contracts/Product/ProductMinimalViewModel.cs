namespace ShopManagement.Application.Contracts.Product
{
    public class ProductMinimalViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Picture { get; set; }
        public string ShortDescription { get; set; }
        public string Code { get; set; }
        public string CreationDateTime { get; set; }
        public decimal UnitPrice { get; set; }
        public string CategoryName { get; set; }
    }
}
