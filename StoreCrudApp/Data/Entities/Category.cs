namespace StoreCrudApp.Data.Entities;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string ImagePath { get; set; } = "";

    public int? ParentId { get; set; }
    public Category? Parent { get; set; }
    public ICollection<Category>? Categories { get; set; }

    public ICollection<Product>? Products { get; set; }

    public bool IsProductCategory => Categories is not null && !Categories.Any();
}
