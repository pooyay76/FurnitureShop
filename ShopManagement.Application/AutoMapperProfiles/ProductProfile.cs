using AutoMapper;
using Framework.Application;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Domain.ProductAgg;

namespace ShopManagement.Application.AutoMapperProfiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductViewModel>()
                .ForMember(dest=>dest.CreationDateTime,opt=>opt.MapFrom(inp=>inp.CreationDateTime.ToFarsi()))
                .ForMember(dest=>dest.CategoryName,opt=>opt.MapFrom(inp => inp.Category.Name));

            CreateMap<Product, ProductMinimalViewModel>()
                .ForMember(dest => dest.CreationDateTime, opt => opt.MapFrom(inp => inp.CreationDateTime.ToFarsi()))
                .ForMember(dest=>dest.CategoryName,opt=>opt.MapFrom(inp => inp.Category.Name));
           
            CreateMap<Product, EditProduct>().ForMember(dest => dest.Picture, opt => opt.Ignore());
        }
    }
}
