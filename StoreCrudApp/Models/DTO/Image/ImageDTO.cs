using StoreCrudApp.Models.DTO.Manufacturer;
using StoreCrudApp.Models.DTO.Product;

namespace StoreCrudApp.Models.DTO.Image;

public class ImageDTO
{
    public int Id { get; set; }
    public string Path { get; set; } = "";

    //public ICollection<ProductDTO>? Products { get; set; }
    //public ICollection<ManufacturerDTO>? Manufacturers { get; set; }
}
