using AutoMapper;
using ShopManagement.Application.Contracts.ProductPicture;
using ShopManagement.Domain.ProductPictureAgg;

namespace ShopManagement.Application.AutoMapperProfiles
{
    public class ProductPictureProfile : Profile
    {
        public ProductPictureProfile()
        {
            CreateMap<ProductPicture, ProductPictureViewModel>().ForMember(dest => dest.ProductName, opt => opt.MapFrom(inp => inp.Product.Name)); ;
            CreateMap<ProductPicture, ProductPictureMinimalViewModel>().ForMember(dest => dest.ProductName, opt => opt.MapFrom(inp => inp.Product.Name)); ;
            CreateMap<ProductPicture, EditProductPicture>().ForMember(dest => dest.Picture, opt => opt.Ignore()); 
        }
    }
}
