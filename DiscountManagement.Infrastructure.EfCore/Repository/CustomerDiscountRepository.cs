using DiscountManagement.Application.Contracts.CustomerDiscount;
using DiscountManagement.Domain.CustomerDiscountAgg;
using Framework.Application;
using Framework.Infrastructure;
using ShopManagement.Infrastructure.EfCore;
using System.Collections.Generic;
using System.Linq;

namespace DiscountManagement.Infrastructure.EfCore.Repository
{
    public class CustomerDiscountRepository : RepositoryBase<long, CustomerDiscount>, ICustomerDiscountRepository
    {
        private readonly DiscountContext discountContext;
        private readonly ShopContext shopContext;
        public CustomerDiscountRepository(DiscountContext discountContext,
            ShopContext shopContext) : base(discountContext)
        {
            this.discountContext = discountContext;
            this.shopContext = shopContext;
        }
        public IEnumerable<CustomerDiscountViewModel> Search(CustomerDiscountSearchModel command)
        {
            IQueryable<CustomerDiscount> query = discountContext.CustomerDiscounts;

            //Filtering Start
            if (command.ProductId != default)
            {
                query = query.Where(x => x.ProductId == command.ProductId);
            }

            //if user has startdate input, show all discounts that were expired after that time
            if (!string.IsNullOrWhiteSpace(command.StartDate))
                query = query.Where(x => x.EndDate >= command.StartDate.ToGeorgianDateTime());

            //if user has enddate input, show all the discounts that were started before that time
            if (!string.IsNullOrWhiteSpace(command.EndDate))
                query = query.Where(x => x.StartDate <= command.EndDate.ToGeorgianDateTime());

            if (!string.IsNullOrWhiteSpace(command.Reason))
                query = query.Where(x => x.Reason.Contains(command.Reason));
            //Filtering End



            var products = shopContext.Products.Select(x => new { x.Name, x.Id }).AsEnumerable();
            //left join query on products


            //                from q in query.AsEnumerable()
            //                join p in products.AsEnumerable().DefaultIfEmpty() on q.ProductId equals p.ProductId
            //                select new CustomerDiscountViewModel

            var result = query.AsEnumerable().Join(products, x => x.ProductId, y => y.Id, (x, y) => new CustomerDiscountViewModel()

            {
                Id = x.Id,
                DiscountPercentage = x.DiscountPercentage,
                EndDate = x.EndDate,
                EndDateFa = x.EndDate.ToFarsi(),
                StartDateFa = x.StartDate.ToFarsi(),
                StartDate = x.StartDate,
                ProductId = x.ProductId,
                Reason = x.Reason,
                ProductName = y.Name
            });



            return result.OrderByDescending(x => x.Id);

        }

    }
}
