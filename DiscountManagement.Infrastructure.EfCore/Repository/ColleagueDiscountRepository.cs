using DiscountManagement.Application.Contracts.ColleagueDiscount;
using DiscountManagement.Domain.ColleagueDiscountAgg;
using Framework.Application;
using Framework.Infrastructure;
using ShopManagement.Infrastructure.EfCore;
using System.Collections.Generic;
using System.Linq;

namespace DiscountManagement.Infrastructure.EfCore.Repository
{
    public class ColleagueDiscountRepository : RepositoryBase<long, ColleagueDiscount>, IColleagueDiscountRepository
    {
        private readonly ShopContext shopContext;
        private readonly DiscountContext discountContext;

        public ColleagueDiscountRepository(ShopContext shopContext, DiscountContext discountContext) : base(discountContext)
        {
            this.shopContext = shopContext;
            this.discountContext = discountContext;
        }
        public List<ColleagueDiscountViewModel> Search(ColleagueDiscountSearchModel entity)
        {
            var query = discountContext.ColleagueDiscounts.AsQueryable();
            var products = shopContext.Products.Select(x => new { x.Id, x.Name }).AsEnumerable();

            if (entity.ProductId != default)
                query = query.Where(x => x.ProductId == entity.ProductId);

            //from dis in query
            //join prod in shopContext.Products on dis.ProductId equals prod.Id
            return query.AsEnumerable().Join(products, x => x.ProductId, y => y.Id, (x, y) => new ColleagueDiscountViewModel
            {
                CreationDate = x.CreationDateTime.ToFarsi(),
                DiscountPercentage = x.DiscountPercentage,
                ProductId = x.ProductId,
                ProductName = y.Name,
                Name = x.Name,
                Id = x.Id
            }).ToList();

        }
    }
}
