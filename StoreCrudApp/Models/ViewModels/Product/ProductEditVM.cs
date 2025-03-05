using Microsoft.AspNetCore.Mvc.Rendering;
using StoreCrudApp.Models.DTO.Product;

namespace StoreCrudApp.Models.ViewModels.Product;

public class ProductEditVM
{
    public ProductEditDTO Product { get; set; } = default!;

    public SelectList? ManufacturerSL { get; set; }
    public MultiSelectList? CategoryMSL { get; set; }
    public int[]? SelectedCategoryIds { get; set; }

    public List<string>? ExistingImagePaths { get; set; } = new();
    public List<string> ImagesToDelete { get; set; } = new();
    public List<IFormFile>? NewImages { get; set; }
}
