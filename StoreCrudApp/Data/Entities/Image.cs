namespace StoreCrudApp.Data.Entities;

public class Image
{
    public int Id { get; set; }
    public string Path { get; set; } = "";

    public ICollection<Product>? Products { get; set; }
    public ICollection<Manufacturer>? Manufacturers { get; set; }

    //public int? ProductId { get; set; }
    //public Product? Product { get; set; }

    //public int? ManufacturerId { get; set; }
    //public Manufacturer? Manufacturer { get; set; }
}
