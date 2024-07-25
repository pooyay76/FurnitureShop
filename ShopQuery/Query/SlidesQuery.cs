using LampshadeQuery.Contracts.Slide;
using ShopManagement.Infrastructure.EfCore;
using System.Collections.Generic;
using System.Linq;

namespace LampshadeQuery.Query
{
    public class SlidesQuery : ISlideQuery
    {
        private readonly ShopContext _context;

        public SlidesQuery(ShopContext context)
        {
            _context = context;
        }

        public List<SlideQueryModel> GetSlides()
        {
            return _context.Slides.Select(x=> new SlideQueryModel { 
            Link = x.Link,
            BtnText = x.BtnText,
            Heading = x.Heading,
            Picture = x.Picture,
            PictureAlt = x.PictureAlt,
            PictureTitle = x.PictureTitle,
            Text = x.Text,
            Title = x.Title
            }).ToList();
        }
    }
}
