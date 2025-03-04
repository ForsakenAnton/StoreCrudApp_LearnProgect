using StoreCrudApp.Models.DTO.Image;
using StoreCrudApp.Models.DTO.Manufacturer;
using System.ComponentModel.DataAnnotations;

namespace StoreCrudApp.Models.DTO.Product;

public class ProductCreateDTO
{
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

    [Range(0, 5)]
    public float Rating { get; set; }

    [Range(0, 10)]
    public int TopLevel { get; set; }

    [Display(Name = "Manufacturer")]
    public int ManufacturerId { get; set; }
}
