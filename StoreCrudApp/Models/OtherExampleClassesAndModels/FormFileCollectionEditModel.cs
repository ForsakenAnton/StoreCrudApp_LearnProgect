namespace StoreCrudApp.Models.OtherExampleClassesAndModels;

public class FormFileCollectionEditModel
{
    public List<string> ExistingImagePaths { get; set; } = new();
    public List<string> ImagesToDelete { get; set; } = new();
    public List<IFormFile> NewImages { get; set; } = new();
}
