using StoreCrudApp.Models.DTO.Category;

namespace StoreCrudApp.Models.ViewModels.Category;

public class CategoryIndexVM
{
    public IEnumerable<CategoryDTO> RootCategories { get; set; } = default!;
}
