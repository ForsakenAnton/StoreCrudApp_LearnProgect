
namespace StoreCrudApp.HostedServices;

public class FileCleanupService : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<FileCleanupService> _logger;

    private readonly string _uploadPath = Path.Combine(
        Directory.GetCurrentDirectory(),
        "wwwroot/uploads");

    private readonly int _cleanUpInterval = 5;

    public FileCleanupService(
        IServiceScopeFactory scopeFactory, 
        ILogger<FileCleanupService> logger)
    {
        _scopeFactory = scopeFactory;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await CleanUpFiles();
                _logger.LogInformation("await CleanUpFiles() works!");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error when clearing files...");
            }

            //await Task.Delay(TimeSpan.FromSeconds(3), stoppingToken);
            await Task.Delay(TimeSpan.FromMinutes(_cleanUpInterval), stoppingToken);
        }
    }

    private async Task CleanUpFiles()
    {
        //using var scope = _scopeFactory.CreateScope();
        //var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationContext>();

        //if (!Directory.Exists(_uploadPath))
        //{
        //    return;
        //}

        //// Get all file paths stored in the database
        //var usedFiles = await dbContext.Products
        //    .SelectMany(p => p.ImagePaths)
        //    .ToListAsync();

        //// Get all the files in the downloads folder
        //string[] files = Directory.GetFiles(_uploadPath);

        //foreach (string file in files)
        //{
        //    string relativePath = "uploads/" + Path.GetFileName(file);

        //    if (!usedFiles.Contains(relativePath))
        //    {
        //        try
        //        {
        //            File.Delete(file);
        //            _logger.LogInformation($"Deleted unused file: {file}");
        //        }
        //        catch (Exception ex)
        //        {
        //            _logger.LogError(ex, $"Error when deleting a file: {file}");
        //        }
        //    }
        //}
    }
}
