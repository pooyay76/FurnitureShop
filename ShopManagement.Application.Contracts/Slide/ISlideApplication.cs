using Framework.Application;
using System.Collections.Generic;

namespace ShopManagement.Application.Contracts.Slide
{
    public interface ISlideApplication
    {
        public OperationResult Create(CreateSlide slide);
        public OperationResult Edit(EditSlide slide);
        public SlideViewModel Get(long id);
        public List<SlideViewModel> List();
        public List<SlideViewModel> Search(SearchSlide query);
        public OperationResult Delete(long id);
        public EditSlide EditGet(long id);
    }
}
