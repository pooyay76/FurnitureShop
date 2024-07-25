using Framework.Infrastructure;
using ShopManagement.Application.Contracts.Slide;
using ShopManagement.Domain.SlideAgg;
using System.Collections.Generic;
using System.Linq;

namespace ShopManagement.Infrastructure.EfCore.Repository
{
    public class SlideRepository : RepositoryBase<long, Slide>, ISlideRepository
    {
        private readonly ShopContext context;

        public SlideRepository(ShopContext context) : base(context)
        {
            this.context = context;
        }

        public Slide GetSlide(long id)
        {
            return context.Slides.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Slide> GetSlides()
        {
            return context.Slides;
        }

        public IEnumerable<Slide> Search(SearchSlide query)
        {
            IQueryable<Slide> items = context.Slides;

            if (!string.IsNullOrWhiteSpace(query.Title))
                items = items.Where(x => x.Text.Contains(query.Title));
            if (!string.IsNullOrWhiteSpace(query.Heading))
                items = items.Where(x => x.Text.Contains(query.Heading));

            return items.OrderByDescending(x => x.Id);

        }


    }
}
