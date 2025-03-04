using System.ComponentModel.DataAnnotations;

namespace StoreCrudApp.Data.Entities;

public class Manufacturer
{
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = "";
    public int? ImageId { get; set; }
    public Image? Image { get; set; }
}
