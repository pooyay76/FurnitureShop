using AutoMapper;
using Framework.Application;
using ShopManagement.Application.Contracts.ProductCategory;
using ShopManagement.Domain.ProductCategoryAgg;

namespace ShopManagement.Application.AutoMapperProfiles
{
    public class ProductCategoryProfile : Profile
    {
        public ProductCategoryProfile()
        {
            CreateMap<ProductCategory, ProductCategoryViewModel>().ForMember(dest => dest.CreationDateTime, opt => opt.MapFrom(inp => inp.CreationDateTime.ToFarsi()));
            CreateMap<ProductCategory, ProductCategoryMinimalViewModel>().ForMember(dest => dest.CreationDateTime, opt => opt.MapFrom(inp => inp.CreationDateTime.ToFarsi()));
            CreateMap<ProductCategory, EditProductCategory>().ForMember(dest => dest.Picture , opt=> opt.Ignore());
        }
    }
}
