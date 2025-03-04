using Microsoft.EntityFrameworkCore;
using StoreCrudApp.Data.Entities;

namespace StoreCrudApp.Data;

public class ApplicationContext : DbContext
{
    public ApplicationContext (DbContextOptions<ApplicationContext> options)
        : base(options)
    {
    }

    public DbSet<Product> Products { get; set; } = default!;
    public DbSet<Manufacturer> Manufacturers { get; set; } = default!;
    public DbSet<Image> Images { get; set; } = default!;
    public DbSet<Category> Categories { get; set; } = default!;


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<Product>()
            .HasQueryFilter(p => !p.IsDeleted);
    }
}
