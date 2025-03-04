using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using StoreCrudApp.Models.DTO.Product;

namespace StoreCrudApp.Models.ViewModels.Product;

public class ProductCreateVM
{
    public ProductCreateDTO Product { get; set; } = default!;
    public SelectList? ManufacturerSL { get; set; }
    public IFormFile[] Files { get; set; } = default!;
}
