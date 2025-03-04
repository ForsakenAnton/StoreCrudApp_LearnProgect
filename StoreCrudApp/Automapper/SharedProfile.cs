using AutoMapper;
using StoreCrudApp.Data.Entities;
using StoreCrudApp.Models.DTO.Category;
using StoreCrudApp.Models.DTO.Image;
using StoreCrudApp.Models.DTO.Manufacturer;
using StoreCrudApp.Models.DTO.Product;

namespace StoreCrudApp.Automapper;

public class SharedProfile : Profile
{
    public SharedProfile()
    {
        CreateMap<Product, ProductDTO>();
        CreateMap<Manufacturer, ManufacturerDTO>();
        CreateMap<Image, ImageDTO>();
        CreateMap<Category, CategoryDTO>();

        CreateMap<ProductCreateDTO, Product>();
        // TODO ProductEditDTO
        CreateMap<Product, ProductDeleteDTO>();
    }
}
