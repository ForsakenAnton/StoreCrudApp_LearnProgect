
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging;
using StoreCrudApp.Data;
using StoreCrudApp.Data.Entities;
using StoreCrudApp.Models.DTO.Category;
using StoreCrudApp.Models.DTO.Manufacturer;
using StoreCrudApp.Models.DTO.Product;
using StoreCrudApp.Models.ViewModels.Product;
using System.Threading.Tasks;

namespace StoreCrudApp.Controllers;

public class ProductsController : Controller
{
    private readonly ApplicationContext _context;
    private readonly IMapper _mapper;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public ProductsController(
        ApplicationContext context,
        IMapper mapper,
        IWebHostEnvironment webHostEnvironment)
    {
        _context = context;
        _mapper = mapper;
        _webHostEnvironment = webHostEnvironment;
    }

    // GET: Products
    public async Task<IActionResult> Index(int categoryId)
    {
        var category = await _context.Categories
            .Include(c => c.Categories)
            .Include(c => c.Products)
            .FirstOrDefaultAsync(c => c.Id == categoryId);

        if (category is not null &&
            !category.IsProductCategory)
        {
            return RedirectToAction(
                actionName: "SubIndex",
                controllerName: "Categories",
                new { categoryId = categoryId });
        }

        var productsEntities = _context.Products
            .Include(p => p.Manufacturer)
                .ThenInclude(m => m!.Image)
            .Include(p => p.Images)
            .Include(p => p.Categories)
            .AsNoTracking();


        // filter
        if (category is not null && category.IsProductCategory)
        {
            productsEntities = productsEntities
                .Where(p => p.Categories!.Any(c => c.Id == category.Id));

            ViewBag.CategoryIdAndName = category.Id + " - " + category.Name;
        }

        var productsDTO = _mapper
            .Map<IEnumerable<ProductDTO>>(await productsEntities.ToListAsync());

        ProductsIndexVM vm = new()
        {
            Products = productsDTO
        };

        return View(vm);
    }

    // GET: Products/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var product = await _context.Products
            .Include(p => p.Manufacturer)
                .ThenInclude(m => m!.Image)
            .Include(p => p.Images)
            .Include(p => p.Categories)
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.Id == id);

        if (product == null)
        {
            return NotFound();
        }

        var productDTO = _mapper
            .Map<ProductDTO>(product);

        ProductDetailsVM vm = new()
        {
            Product = productDTO
        };

        return View(vm);
    }




    // GET: Products/Create
    public async Task<IActionResult> Create()
    {
        var manufacturers = await _context.Manufacturers.ToListAsync();
        var manufacturersDTO = _mapper.Map<IEnumerable<ManufacturerDTO>>(manufacturers);

        SelectList manufacturerSL = new SelectList(
            items: manufacturersDTO,
            dataValueField: "Id",
            dataTextField: "Name");


        var categories = await _context.Categories
            //.Where(c => c.Categories == null)
            .ToListAsync();

        categories = categories.Where(c => c.Categories == null).ToList();

        var categoriesDTO = _mapper.Map<IEnumerable<CategoryDTO>>(categories);

        MultiSelectList categoryMSL = new MultiSelectList(
           items: categoriesDTO,
           dataValueField: "Id",
           dataTextField: "Name");

        ProductCreateVM vm = new()
        {
            ManufacturerSL = manufacturerSL,
            CategoryMSL = categoryMSL
        };


        return View(vm);
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ProductCreateVM vm)
    {
        if (!ModelState.IsValid)
        {
            var manufacturers = await _context.Manufacturers.ToListAsync();
            var manufacturersDTO = _mapper.Map<IEnumerable<ManufacturerDTO>>(manufacturers);

            SelectList manufacturerSL = new SelectList(
                items: manufacturersDTO,
                dataValueField: "Id",
                dataTextField: "Name",
                selectedValue: vm.Product.ManufacturerId);

            vm.ManufacturerSL = manufacturerSL;


            var categories = await _context.Categories
                //.Where(c => c.Categories == null)
                .ToListAsync();

            categories = categories.Where(c => c.Categories == null).ToList();

            var categoriesDTO = _mapper.Map<IEnumerable<CategoryDTO>>(categories);

            MultiSelectList categoryMSL = new MultiSelectList(
               items: categoriesDTO,
               dataValueField: "Id",
               dataTextField: "Name");

            vm.CategoryMSL = categoryMSL;

            return View(vm);
        }

        string wwwroot = _webHostEnvironment.WebRootPath;
        string filesDir = "images";


        foreach (IFormFile file in vm.Files)
        {
            string fileName = $"{wwwroot}/{filesDir}/{file.FileName}";
            using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                await file.CopyToAsync(fs);
            }
        }

        IEnumerable<Image> images = vm.Files.Select(f => new Image
        {
            Path = $"/{filesDir}/{f.FileName}"
        });

        List<Category> selectedCategories = new List<Category>();

        foreach (int id in vm.SelectedCategoryIds ?? [])
        {
            var selectedCategory = await _context.Categories
                .FirstOrDefaultAsync(c => c.Id == id);
            if (selectedCategory is not null)
            {
                selectedCategories.Add(selectedCategory);
            }
        }


        Product product = _mapper.Map<Product>(vm.Product);
        product.Images = images.ToList();
        product.Categories = selectedCategories;

        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }






    // GET: Products/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var product = await _context.Products
            .Include(p => p.Manufacturer)
            .Include(p => p.Categories)
            .Include(p => p.Images)
            .FirstOrDefaultAsync(p => p.Id == id);
        if (product == null)
        {
            return NotFound();
        }

        var manufacturers = await _context.Manufacturers.ToListAsync();
        var manufacturersDTO = _mapper.Map<IEnumerable<ManufacturerDTO>>(manufacturers);

        SelectList manufacturerSL = new SelectList(
            items: manufacturersDTO,
            dataValueField: "Id",
            dataTextField: "Name",
            selectedValue: product.ManufacturerId);


        var categories = await _context.Categories
            //.Where(c => c.Categories == null)
            .ToListAsync();

        categories = categories.Where(c => c.Categories == null).ToList();
        var categoriesDTO = _mapper.Map<IEnumerable<CategoryDTO>>(categories);

        MultiSelectList categoryMSL = new MultiSelectList(
           items: categoriesDTO,
           dataValueField: "Id",
           dataTextField: "Name",
           selectedValues: product.Categories);

        IEnumerable<string>? existingImagePaths = product.Images?
            .Select(i => i.Path);

        IEnumerable<int>? selectedCategoryIds = product.Categories?
            .Select(c => c.Id);

        ProductEditDTO productEditDTO = _mapper.Map<ProductEditDTO>(product);

        ProductEditVM vm = new()
        {
            Product = productEditDTO,
            ManufacturerSL = manufacturerSL,
            CategoryMSL = categoryMSL,
            SelectedCategoryIds = selectedCategoryIds?.ToArray(),
            ExistingImagePaths = existingImagePaths?.ToList(),
        };

        return View(vm);
    }

    // POST: Products/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, ProductEditVM vm)
    {
        if (id != vm.Product.Id)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            var manufacturers = await _context.Manufacturers.ToListAsync();
            var manufacturersDTO = _mapper.Map<IEnumerable<ManufacturerDTO>>(manufacturers);

            SelectList manufacturerSL = new SelectList(
                items: manufacturersDTO,
                dataValueField: "Id",
                dataTextField: "Name",
                selectedValue: vm.Product.ManufacturerId);

            vm.ManufacturerSL = manufacturerSL;


            var categories = await _context.Categories
                .ToListAsync();

            categories = categories.Where(c => c.Categories == null).ToList();
            var categoriesDTO = _mapper.Map<IEnumerable<CategoryDTO>>(categories);

            MultiSelectList categoryMSL = new MultiSelectList(
               items: categoriesDTO,
               dataValueField: "Id",
               dataTextField: "Name");

            vm.CategoryMSL = categoryMSL;

            return View(vm);
        }

        try
        {
            string wwwroot = _webHostEnvironment.WebRootPath;
            string filesDir = "images";

            foreach (IFormFile file in vm.NewImages ?? [])
            {
                string fileName = $"{wwwroot}/{filesDir}/{file.FileName}";
                using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
                {
                    await file.CopyToAsync(fs);
                }
            }

            // Get the product and track its changes
            Product? existingProduct = await _context.Products
                .Include(p => p.Categories)
                .Include(p => p.Images)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (existingProduct is null)
            {
                return NotFound();
            }

            // Mapping
            _mapper.Map(
                source: vm.Product,
                destination: existingProduct);

            // Create images and categories to add/change ////////////////////////
            IEnumerable<Image>? imagesToAdd = vm.NewImages?.Select(f => new Image
            {
                Path = $"/{filesDir}/{f.FileName}"
            });

            List<Category> selectedCategoriesToAdd = await _context.Categories
                .Where(c => vm.SelectedCategoryIds!
                .Any(sc => sc == c.Id)).ToListAsync();
            // ///////////////////////////////////////////////////////////////////


            //IEnumerable<string>? intersectedImagesToDelete = vm.ExistingImagePaths?
            //    .Intersect(vm.ImagesToDelete);

            //foreach (string imagePath in intersectedImagesToDelete ?? [])
            //{
            //    Image? imageToDelete = existingProduct.Images?
            //        .FirstOrDefault(i => i.Path == imagePath);
            //    if (imageToDelete is not null)
            //    {
            //        existingProduct.Images?.Remove(imageToDelete);
            //    }
            //}


            // Elegant removal the imagesToDelete from the tracking product (EF Core traching) using LINQ 
            existingProduct.Images = existingProduct.Images?
                .Where(image => !(vm.ImagesToDelete ?? []).Contains(image.Path))
                .ToList();


            // Add images and categories
            // (the manufacturer is already changed in the Product.ManufacturerId field)
            if (imagesToAdd is not null)
            {
                existingProduct.Images.AddRange(imagesToAdd);
            }

            existingProduct.Categories?.Clear();
            existingProduct.Categories.AddRange(selectedCategoriesToAdd);
            // ////////////////////////////////////////////////////////////////////////////////

            _context.Products.Update(existingProduct);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        catch (DbUpdateConcurrencyException ex)
        {
            Console.WriteLine(ex.Message);
        }

        return RedirectToAction(nameof(Index));
    }



    // GET: Products/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var product = await _context.Products
            .Include(p => p.Manufacturer)
            //.Include(p => p.Images)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (product == null)
        {
            return NotFound();
        }

        ProductDeleteDTO productDeleteDTO = _mapper.Map<ProductDeleteDTO>(product);

        ProductDeleteVM vm = new()
        {
            Product = productDeleteDTO
        };

        return View(vm);
    }

    // POST: Products/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product != null)
        {
            //_context.Products.Remove(product);
            product.IsDeleted = true;
        }

        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }





    private bool ProductExists(int id)
    {
        return _context.Products.Any(e => e.Id == id);
    }
}
