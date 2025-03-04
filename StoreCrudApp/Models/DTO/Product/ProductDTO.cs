using StoreCrudApp.Data.Entities;
using StoreCrudApp.Models.DTO.Category;
using StoreCrudApp.Models.DTO.Image;
using StoreCrudApp.Models.DTO.Manufacturer;
using System.ComponentModel.DataAnnotations;

namespace StoreCrudApp.Models.DTO.Product;



public class ProductDTO
{
    public int Id { get; set; }

    [Required]
    [MinLength(2)]
    [MaxLength(100)]
    public string Name { get; set; } = "";

    [MaxLength(10000)]
    public string Description { get; set; } = "";

    [Range(0, 100_100)]
    [DataType(DataType.Currency)]
    [DisplayFormat(DataFormatString = "{0:C2}"/*, ApplyFormatInEditMode = true*/)]
    public decimal Price { get; set; }

    [Range(0, 100)]
    public int DiscountPercent { get; set; }

    [DataType(DataType.Date)]
    public DateTime CreationDate { get; set; } = DateTime.Now;

    [Range(0, 5)]
    public float Rating { get; set; }

    [Range(0, 10)]
    public int TopLevel { get; set; }

    public ICollection<ImageDTO>? Images { get; set; }

    public int ManufacturerId { get; set; }
    public ManufacturerDTO? Manufacturer { get; set; }
    public ICollection<CategoryDTO>? Categories { get; set; }

    //public int CategoryId { get; set; }
    //public Category? Category { get; set; }

    public decimal Discount => Price - Price * ((decimal)DiscountPercent / 100);
}


