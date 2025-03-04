using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreCrudApp.Data.Entities;

public class Product
{
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = "";

    [MaxLength(10000)]
    public string Description { get; set; } = "";

    [Column(TypeName = "decimal(18, 2)")]
    public decimal Price { get; set; }

    public int DiscountPercent { get; set; }

    public DateTime CreationDate { get; set; } = DateTime.Now;

    public float Rating { get; set; }

    public int TopLevel { get; set; }

    public ICollection<Image>? Images { get; set; }

    public int ManufacturerId { get; set; }
    public Manufacturer? Manufacturer { get; set; }
    public ICollection<Category>? Categories { get; set; }

    //public int CategoryId { get; set; }
    //public Category? Category { get; set; }

    public decimal Discount => Price - Price * ((decimal)DiscountPercent / 100);

    public bool IsDeleted { get; set; }
}
