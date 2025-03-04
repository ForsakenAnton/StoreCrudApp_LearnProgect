using StoreCrudApp.Models.DTO.Product;

namespace StoreCrudApp.Models.ViewModels.Product;

public class ProductsIndexVM
{
    public IEnumerable<ProductDTO>? Products { get; set; }
}
