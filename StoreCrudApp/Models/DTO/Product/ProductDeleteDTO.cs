using StoreCrudApp.Models.DTO.Category;
using StoreCrudApp.Models.DTO.Image;
using StoreCrudApp.Models.DTO.Manufacturer;
using System.ComponentModel.DataAnnotations;

namespace StoreCrudApp.Models.DTO.Product;

public class ProductDeleteDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = "";

    public string Description { get; set; } = "";

    [DataType(DataType.Currency)]
    [DisplayFormat(DataFormatString = "{0:C2}"/*, ApplyFormatInEditMode = true*/)]
    public decimal Price { get; set; }

    public int DiscountPercent { get; set; }

    [DataType(DataType.Date)]
    public DateTime CreationDate { get; set; } = DateTime.Now;

    public float Rating { get; set; }

    public int TopLevel { get; set; }

    public ICollection<ImageDTO>? Images { get; set; }

    public int ManufacturerId { get; set; }
    public ManufacturerDTO? Manufacturer { get; set; }
    public ICollection<CategoryDTO>? Categories { get; set; }

    public decimal Discount => Price - Price * ((decimal)DiscountPercent / 100);
}
