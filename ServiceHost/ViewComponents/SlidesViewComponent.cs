using LampshadeQuery.Contracts.Slide;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.ViewComponents
{
    public class SlidesViewComponent : ViewComponent
    {
        private readonly ISlideQuery slideQuery;

        public SlidesViewComponent(ISlideQuery slideQuery)
        {
            this.slideQuery = slideQuery;
        }
        public IViewComponentResult Invoke()
        {
            var Slides = slideQuery.GetSlides();
            return View(Slides);
        }
    }

}
