using Microsoft.AspNetCore.Mvc;
using StoreCrudApp.Models.OtherExampleClassesAndModels;

namespace StoreCrudApp.Controllers;

public class DecimalController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        DecimalModel model = new() { Value = 123.12M };

 
        return View(model);
    }

    [HttpPost]
    public IActionResult Index(DecimalModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        return Json(model);
    }
}
