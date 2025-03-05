using Microsoft.AspNetCore.Localization;
using System.Globalization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StoreCrudApp.Data;
using StoreCrudApp.Automapper;
using StoreCrudApp.HostedServices;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddDbContext<ApplicationContext>(options =>
{
    string cs = builder.Configuration.GetConnectionString("ApplicationContext")!;
    options.UseSqlServer(cs);
});

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddHostedService<FileCleanupService>();


builder.Services.AddControllersWithViews();
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

//builder.Services.AddLocalization();

var app = builder.Build();

app.UseRequestLocalization(localizationOptions =>
{
    //var defaultCulture = new CultureInfo(CultureInfo.InvariantCulture.Name);
    var defaultCulture = new CultureInfo("en-US");

    localizationOptions.DefaultRequestCulture = new RequestCulture(defaultCulture);
    localizationOptions.SupportedCultures = new List<CultureInfo> { defaultCulture };
    localizationOptions.SupportedUICultures = new List<CultureInfo> { defaultCulture };
});

using (var scope = app.Services.CreateScope())
{
    var sp = scope.ServiceProvider;
    var context = sp.GetRequiredService<ApplicationContext>();
    //await context.Database.EnsureDeletedAsync();
    await context.Database.EnsureCreatedAsync();

    await DbInitializer.Init(context);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id:int?}");

await app.RunAsync();
