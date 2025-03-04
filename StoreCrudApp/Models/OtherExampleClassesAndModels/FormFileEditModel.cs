namespace StoreCrudApp.Models.OtherExampleClassesAndModels;

public class FormFileEditModel
{
    public string ExistingImagePath { get; set; } = "";
    public IFormFile? NewImage { get; set; }
}
