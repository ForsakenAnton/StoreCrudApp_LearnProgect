using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreCrudApp.Models.OtherExampleClassesAndModels;

namespace StoreCrudApp.Controllers;

public class IFormFilesController : Controller
{
    private readonly string exampleImagesRootDirectory;

    public IFormFilesController(IWebHostEnvironment webHostEnvironment)
    {
        exampleImagesRootDirectory = $"{webHostEnvironment.WebRootPath}/exampleImages";
    }

    public IActionResult Index()
    {
        return View();
    }



    [HttpGet]
    public IActionResult UploadFile()
    {
        var model = new FormFileEditModel
        {
            ExistingImagePath = "/exampleImages/cat1.jpg"
        };

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> UploadFile(FormFileEditModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        if (model.NewImage != null)
        {
            string filePath = Path.Combine(
                exampleImagesRootDirectory,
                model.NewImage.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await model.NewImage.CopyToAsync(stream);
            }

            model.ExistingImagePath = $"/exampleImages/{model.NewImage.FileName}";
        }

        return View(model);
    }





    [HttpGet]
    public IActionResult UploadFileCollection()
    {
        var model = new FormFileCollectionEditModel
        {
            ExistingImagePaths =
            [
                "/exampleImages/cat1.jpg",
                "/exampleImages/cat2.jpg",
                "/exampleImages/cat3.jpg",
            ]
        };

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> UploadFileCollection(FormFileCollectionEditModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        if (model.ImagesToDelete != null && model.ImagesToDelete.Count > 0)
        {
            foreach (string imagePath in model.ImagesToDelete)
            {
                string fullPath = Path.Combine(
                    exampleImagesRootDirectory, 
                    Path.GetFileName(imagePath));

                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }
            }
        }

        if (model.NewImages != null && model.NewImages.Count > 0)
        {
            foreach (var image in model.NewImages)
            {
                string filePath = Path.Combine(exampleImagesRootDirectory, image.FileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }
            }
        }

        model.ExistingImagePaths = Directory
            .GetFiles(exampleImagesRootDirectory)
            .Select(file => $"/exampleImages/{Path.GetFileName(file)}")
            .ToList();


        return View(model);
    }
}
