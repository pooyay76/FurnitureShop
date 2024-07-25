using AutoMapper;
using Framework.Application;
using ShopManagement.Application.Contracts.Slide;
using ShopManagement.Domain.SlideAgg;
using System.Collections.Generic;
using System.Linq;

namespace ShopManagement.Application
{
    public class SlideApplication : ISlideApplication
    {
        private readonly ISlideRepository slideRepository;
        private readonly IMapper mapper;
        private readonly IFileUploader fileUploader;
        private const string filePath = "Slide";
        public SlideApplication(ISlideRepository slideRepository, IMapper mapper, IFileUploader fileUploader)
        {
            this.slideRepository = slideRepository;
            this.mapper = mapper;
            this.fileUploader = fileUploader;
        }

        public OperationResult Create(CreateSlide slide)
        {
            var operation = new OperationResult();
            string fileName = fileUploader.Upload(slide.Picture, filePath);
            var entity = new Slide(fileName, slide.Link, slide.PictureTitle, slide.PictureAlt, slide.Heading,
                slide.Title, slide.Text, slide.BtnText);
            slideRepository.Create(entity);
            return operation.Succeeded();
        }
        public OperationResult Delete(long id)
        {
            OperationResult op = new();
            var product = slideRepository.Get(id);
            if (product == null)
                return op.Failed(ApplicationMessages.NotFoundMessage);
            else
            {
                slideRepository.Delete(product);
                return op.Succeeded();
            }
        }
        public OperationResult Edit(EditSlide command)
        {
            var operation = new OperationResult();
            var target = slideRepository.GetSlide(command.Id);
            if (target == null)
                return operation.Failed(ApplicationMessages.NotFoundMessage);
            string fileName = fileUploader.Upload(command.Picture, filePath);
            target.Edit(fileName, command.Link, command.PictureTitle, command.PictureAlt, command.Heading,
                command.Title, command.Text, command.BtnText);
            slideRepository.Update(target);
            return operation.Succeeded();
        }

        public EditSlide EditGet(long id)
        {
            var target = slideRepository.GetSlide(id);
            if (target == null)
                return null;
            return mapper.Map<Slide, EditSlide>(target);
        }

        public SlideViewModel Get(long id)
        {
            var target = slideRepository.GetSlide(id);
            if (target == null)
                return null;
            return mapper.Map<Slide, SlideViewModel>(target);
        }

        public List<SlideViewModel> List()
        {
            return slideRepository.GetSlides().Select(x => mapper.Map<Slide, SlideViewModel>(x)).ToList();
        }

        public List<SlideViewModel> Search(SearchSlide query)
        {
            return slideRepository.Search(query).Select(x => mapper.Map<Slide, SlideViewModel>(x)).ToList();
        }
    }
}
