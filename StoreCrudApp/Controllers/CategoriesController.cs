using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreCrudApp.Data;
using StoreCrudApp.Data.Entities;
using StoreCrudApp.Models.DTO.Category;
using StoreCrudApp.Models.ViewModels.Category;
using System.Threading.Tasks;

namespace StoreCrudApp.Controllers;

public class CategoriesController : Controller
{
    private readonly ApplicationContext _context;
    private readonly IMapper _mapper;

    public CategoriesController(ApplicationContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IActionResult> Index()
    {
        var rootCategories = await _context.Categories
            .Where(c => c.ParentId == null)
            .ToListAsync();

        CategoryIndexVM vm = new()
        {
            RootCategories = _mapper.Map<IEnumerable<CategoryDTO>>(rootCategories)
        };

        return View(model: vm);
    }

    public async Task<IActionResult> SubIndex(int categoryId)
    {
        if (categoryId == 0)
        {
            return RedirectToAction(
                actionName: "Index",
                controllerName: "Products");
        }

        var category = await _context.Categories
            .Include(c => c.Categories)
            .FirstOrDefaultAsync(c => c.Id == categoryId);

        if (category is not null &&
            !category.IsProductCategory)
        {
            CategorySubIndexVM vm = new()
            {
                Category = _mapper.Map<CategoryDTO>(category)
            };

            return View(model: vm);
        }

        return RedirectToAction(
            actionName: "Index",
            controllerName: "Products",
            new { categoryId = categoryId });
    }
}
