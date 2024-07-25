using AutoMapper;
using Framework.Application;
using ShopManagement.Application.Contracts.Slide;
using ShopManagement.Domain.SlideAgg;

namespace ShopManagement.Application.AutoMapperProfiles
{
    public class SlideProfile : Profile
    {
        public SlideProfile()
        {
            CreateMap<Slide, SlideViewModel>()
                .ForMember(dest => dest.CreationDateTime, opt => opt.MapFrom(inp => inp.CreationDateTime.ToFarsi()));
;
            CreateMap<Slide, SlideMinimalViewModel>()
                .ForMember(dest => dest.CreationDateTime, opt => opt.MapFrom(inp => inp.CreationDateTime.ToFarsi()));
;
            CreateMap<Slide, EditSlide>().ForMember(dest => dest.Picture, opt => opt.Ignore());
        }
    }
}
