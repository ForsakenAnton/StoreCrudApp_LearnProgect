using StoreCrudApp.Models.DTO.Image;
using System.ComponentModel.DataAnnotations;

namespace StoreCrudApp.Models.DTO.Manufacturer;

public class ManufacturerDTO
{
    public int Id { get; set; }

    [Required]
    [MinLength(2)]
    [MaxLength(100)]
    public string Name { get; set; } = "";
    public int? ImageId { get; set; }
    public ImageDTO? Image { get; set; }
}
