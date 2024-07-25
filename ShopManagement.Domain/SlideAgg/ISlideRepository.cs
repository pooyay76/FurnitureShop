using Framework.Domain;
using ShopManagement.Application.Contracts.Slide;
using System.Collections.Generic;

namespace ShopManagement.Domain.SlideAgg
{
    public interface ISlideRepository:IRepository<long,Slide>
    {
        public Slide GetSlide(long id);

        public IEnumerable<Slide> Search(SearchSlide slide);
        public IEnumerable<Slide> GetSlides();
    }
}
