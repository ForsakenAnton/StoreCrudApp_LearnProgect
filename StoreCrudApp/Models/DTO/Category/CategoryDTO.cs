using StoreCrudApp.Models.DTO.Product;

namespace StoreCrudApp.Models.DTO.Category;

public class CategoryDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string ImagePath { get; set; } = "";

    public int? ParentId { get; set; }
    public CategoryDTO? Parent { get; set; }
    public ICollection<CategoryDTO>? Categories { get; set; }

    public ICollection<ProductDTO>? Products { get; set; }

    public bool IsProductCategory => Categories is not null && !Categories.Any();

}
