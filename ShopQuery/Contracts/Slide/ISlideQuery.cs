using System.Collections.Generic;

namespace LampshadeQuery.Contracts.Slide
{
    public interface ISlideQuery
    {
        public List<SlideQueryModel> GetSlides();
    }
}
