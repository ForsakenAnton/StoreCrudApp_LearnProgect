
using Humanizer;
using Microsoft.EntityFrameworkCore;
using StoreCrudApp.Data.Entities;
using System.Reflection.Metadata;

namespace StoreCrudApp.Data;

public static class DbInitializer
{
    public static async Task Init(ApplicationContext context)
    {
        if (await context.Products.AnyAsync())
        {
            return;
        }

        List<Image> images = new()
        {
            new Image{ Path = "/images/appleLogo.png"},
            new Image{ Path = "/images/bluettiLogo.png"},
            new Image{ Path = "/images/googlePixelLogo.png"},
            new Image{ Path = "/images/huaweiLogo.png"},
            new Image{ Path = "/images/samsungLogo.png"},
            new Image{ Path = "/images/appleProduct1.jpg"},
            new Image{ Path = "/images/bluettiProduct1.jpg"},
            new Image{ Path = "/images/bluettiProduct2.jpg"},
            new Image{ Path = "/images/googlePixelProduct1.jpg"},
            new Image{ Path = "/images/googlePixelProduct2.jpg"},
            new Image{ Path = "/images/googlePixelProduct3.jpg"},
            new Image{ Path = "/images/huaweiProduct1.jpg"},
            new Image{ Path = "/images/huaweiProduct2.jpg"},
            new Image{ Path = "/images/huaweiProduct3.jpg"},
            new Image{ Path = "/images/samsungProduct1.jpg"},
            new Image{ Path = "/images/samsungProduct2.jpg"},
            new Image{ Path = "/images/samsungProduct3.jpg"},
        };

        string imagePlaceholderPath = "/images/imagePlaceholder.jpg";

        await context.Images.AddRangeAsync(images);
        await context.SaveChangesAsync();

        List<Manufacturer> manufacturers = new()
        {
            new Manufacturer { Name = "Apple", Image = images[0]},
            new Manufacturer { Name = "Bluetti", Image = images[1]},
            new Manufacturer { Name = "Google Pixel", Image = images[2]},
            new Manufacturer { Name = "Huawei", Image = images[3]},
            new Manufacturer { Name = "Samsung", Image = images[4]},
        };

        await context.Manufacturers.AddRangeAsync(manufacturers);
        await context.SaveChangesAsync();



        List<Category> categories = new List<Category>
            {
                new Category
                {
                    Name = "Electronic",
                    //ParentCommentId = null,
                    ParentId = null,
                    ImagePath = imagePlaceholderPath,
                    Categories = new List<Category>()
                    {
                        new Category
                        {
                            Name = "Electronic For Home",
                            ImagePath = imagePlaceholderPath,
                            Categories = new List<Category>()
                            {
                                new Category
                                {
                                    Name = "Dishwashers",
                                    ImagePath = imagePlaceholderPath
                                },
                                new Category
                                {
                                    Name = "Charger Stations",
                                    ImagePath = imagePlaceholderPath
                                },
                                new Category
                                {
                                    Name = "Hoovers",
                                    ImagePath = imagePlaceholderPath
                                },
                            }
                        },
                        new Category
                        {
                            Name = "Gadgets",
                            ImagePath = imagePlaceholderPath,
                            Categories = new List<Category>()
                            {
                                new Category
                                {
                                    Name = "All gadgets",
                                    ImagePath = imagePlaceholderPath
                                },
                                new Category
                                {
                                    Name = "Phones",
                                    ImagePath = imagePlaceholderPath,
                                    Categories = new List<Category>()
                                    {
                                        new Category
                                        {
                                            Name = "Samsung Phones",
                                            ImagePath = imagePlaceholderPath
                                        },
                                        new Category
                                        {
                                            Name = "Huawei Phones",
                                            ImagePath = imagePlaceholderPath
                                        },
                                        new Category
                                        {
                                            Name = "Google Phones",
                                            ImagePath = imagePlaceholderPath
                                        },
                                    }
                                }
                            }
                        },
                    }
                },
                new Category
                {
                    Name = "Kid toys",
                    ImagePath = imagePlaceholderPath,
                },
                new Category
                {
                    Name = "Top",
                    ImagePath = imagePlaceholderPath
                }
            };

        await context.AddRangeAsync(categories);
        await context.SaveChangesAsync();


        var categoryListFromDB = await context.Categories.ToListAsync();


        List<Product> products = new()
        {
            new Product
            {
                Name = "Apple 16",
                Description = "Powerful device",
                Price = 100500.99M,
                DiscountPercent = 5,
                CreationDate = new DateTime(2024, 3, 10),
                Rating = 3,
                TopLevel = 9,
                Images = images.Where(i => i.Path.Contains("appleProduct")).ToList(),
                Manufacturer = manufacturers.First(m => m.Name == "Apple"),
                Categories = categoryListFromDB
                    .Where(c => c.Name == "Top")
                    .ToList()
            },
            new Product
            {
                Name = "Bluetti Power 1",
                Description = "Solar Generator",
                Price = 4500.50M,
                DiscountPercent = 10,
                CreationDate = new DateTime(2024, 2, 15),
                Rating = 4.5f,
                TopLevel = 7,
                Images = images.Where(i => i.Path.Contains("bluettiProduct")).ToList(),
                Manufacturer = manufacturers.First(m => m.Name == "Bluetti"),
                Categories = categoryListFromDB
                    .Where(c =>
                        c.Name == "Charger Stations" ||
                        c.Name == "Top")
                    .ToList()
            },
            new Product
            {
                Name = "Bluetti Power 2",
                Description = "High capacity battery",
                Price = 5500.75M,
                DiscountPercent = 8,
                CreationDate = new DateTime(2024, 1, 20),
                Rating = 4,
                TopLevel = 6,
                Images = images.Where(i => i.Path.Contains("bluettiProduct")).ToList(),
                Manufacturer = manufacturers.First(m => m.Name == "Bluetti"),
                Categories = categoryListFromDB
                    .Where(c =>
                        c.Name == "Charger Stations" ||
                        c.Name == "Top")
                    .ToList()
            },
            new Product
            {
                Name = "Google Pixel 1",
                Description = "New generation smartphone",
                Price = 3200.99M,
                DiscountPercent = 7,
                CreationDate = new DateTime(2024, 5, 5),
                Rating = 4.2f,
                TopLevel = 8,
                Images = images.Where(i => i.Path.Contains("googlePixelProduct")).ToList(),
                Manufacturer = manufacturers.First(m => m.Name == "Google Pixel"),
                Categories = categoryListFromDB
                    .Where(c =>
                        c.Name == "All gadgets" ||
                        c.Name == "Google Phones" ||
                        c.Name == "Top")
                    .ToList()
            },
            new Product
            {
                Name = "Google Pixel 2",
                Description = "Improved camera",
                Price = 3700.49M,
                DiscountPercent = 6,
                CreationDate = new DateTime(2024, 6, 1),
                Rating = 4.5f,
                TopLevel = 9,
                Images = images.Where(i => i.Path.Contains("googlePixelProduct")).ToList(),
                Manufacturer = manufacturers.First(m => m.Name == "Google Pixel"),
                Categories = categoryListFromDB
                    .Where(c =>
                        c.Name == "All gadgets" ||
                        c.Name == "Google Phones" ||
                        c.Name == "Top")
                    .ToList()
            },
            new Product
            {
                Name = "Google Pixel 3",
                Description = "AI features",
                Price = 4200.00M,
                DiscountPercent = 5,
                CreationDate = new DateTime(2024, 7, 1),
                Rating = 4.8f,
                TopLevel = 10,
                Images = images.Where(i => i.Path.Contains("googlePixelProduct")).ToList(),
                Manufacturer = manufacturers.First(m => m.Name == "Google Pixel"),
                Categories = categoryListFromDB
                    .Where(c =>
                        c.Name == "All gadgets" ||
                        c.Name == "Google Phones" ||
                        c.Name == "Top")
                    .ToList()
            },
            new Product
            {
                Name = "Huawei Mate 1",
                Description = "Flagship device",
                Price = 5400.30M,
                DiscountPercent = 12,
                CreationDate = new DateTime(2024, 3, 8),
                Rating = 4.3f,
                TopLevel = 7,
                Images = images.Where(i => i.Path.Contains("huaweiProduct")).ToList(),
                Manufacturer = manufacturers.First(m => m.Name == "Huawei"),
                Categories = categoryListFromDB
                    .Where(c =>
                        c.Name == "All gadgets" ||
                        c.Name == "Huawei Phones")
                    .ToList()
            },
            new Product
            {
                Name = "Huawei Mate 2",
                Description = "New tech",
                Price = 6000.90M,
                DiscountPercent = 9,
                CreationDate = new DateTime(2024, 4, 10),
                Rating = 4.4f,
                TopLevel = 8,
                Images = images.Where(i => i.Path.Contains("huaweiProduct")).ToList(),
                Manufacturer = manufacturers.First(m => m.Name == "Huawei"),
                Categories = categoryListFromDB
                    .Where(c =>
                        c.Name == "All gadgets" ||
                        c.Name == "Huawei Phones")
                    .ToList()
            },
            new Product
            {
                Name = "Huawei Mate 3",
                Description = "Premium edition",
                Price = 7200.15M,
                DiscountPercent = 11,
                CreationDate = new DateTime(2024, 5, 15),
                Rating = 4.6f,
                TopLevel = 9,
                Images = images.Where(i => i.Path.Contains("huaweiProduct")).ToList(),
                Manufacturer = manufacturers.First(m => m.Name == "Huawei"),
                Categories = categoryListFromDB
                    .Where(c =>
                        c.Name == "All gadgets" ||
                        c.Name == "Huawei Phones")
                    .ToList()
            },
            new Product
            {
                Name = "Samsung Galaxy 1",
                Description = "Next-gen display",
                Price = 4800.80M,
                DiscountPercent = 7,
                CreationDate = new DateTime(2024, 2, 18),
                Rating = 4.1f,
                TopLevel = 6,
                Images = images.Where(i => i.Path.Contains("samsungProduct")).ToList(),
                Manufacturer = manufacturers.First(m => m.Name == "Samsung"),
                Categories = categoryListFromDB
                    .Where(c =>
                        c.Name == "All gadgets" ||
                        c.Name == "Samsung Phones" ||
                        c.Name == "Top")
                    .ToList()
            },
            new Product
            {
                Name = "Samsung Galaxy 2",
                Description = "Better battery",
                Price = 5300.60M,
                DiscountPercent = 8,
                CreationDate = new DateTime(2024, 3, 22),
                Rating = 4.5f,
                TopLevel = 8,
                Images = images.Where(i => i.Path.Contains("samsungProduct")).ToList(),
                Manufacturer = manufacturers.First(m => m.Name == "Samsung"),
                Categories = categoryListFromDB
                    .Where(c =>
                        c.Name == "All gadgets" ||
                        c.Name == "Samsung Phones" ||
                        c.Name == "Top")
                    .ToList()
            },
            new Product
            {
                Name = "Samsung Galaxy 3",
                Description = "Top performance",
                Price = 6800.25M,
                DiscountPercent = 9,
                CreationDate = new DateTime(2024, 4, 25),
                Rating = 4.7f,
                TopLevel = 10,
                Images = images.Where(i => i.Path.Contains("samsungProduct")).ToList(),
                Manufacturer = manufacturers.First(m => m.Name == "Samsung"),
                Categories = categoryListFromDB
                    .Where(c =>
                        c.Name == "All gadgets" ||
                        c.Name == "Samsung Phones" ||
                        c.Name == "Top")
                    .ToList()
            }
        };


        await context.Products.AddRangeAsync(products);
        await context.SaveChangesAsync();
    }
}
















//using Microsoft.EntityFrameworkCore;
//using StoreCrudApp.Data.Entities;

//namespace StoreCrudApp.Data;

//public static class DbInitializer
//{
//    public static async Task Init(ApplicationContext context)
//    {
//        if (await context.Products.AnyAsync())
//        {
//            return;
//        }

//        string appleLogo = "/images/appleLogo.png";
//        string bluettiLogo = "/images/bluettiLogo.png";
//        string googlePixelLogo = "/images/googlePixelLogo.png";
//        string huaweiLogo = "/images/huaweiLogo.png";
//        string samsungLogo = "/images/samsungLogo.png";

//        string appleProduct1 = "/images/appleProduct1.jpg";
//        string bluettiProduct1 = "/images/bluettiProduct1.jpg";
//        string bluettiProduct2 = "/images/bluettiProduct2.jpg";
//        string googlePixelProduct1 = "/images/googlePixelProduct1.jpg";
//        string googlePixelProduct2 = "/images/googlePixelProduct2.jpg";
//        string googlePixelProduct3 = "/images/googlePixelProduct3.jpg";
//        string huaweiProduct1 = "/images/huaweiProduct1.jpg";
//        string huaweiProduct2 = "/images/huaweiProduct2.jpg";
//        string huaweiProduct3 = "/images/huaweiProduct3.jpg";
//        string samsungProduct1 = "/images/samsungProduct1.jpg";
//        string samsungProduct2 = "/images/samsungProduct2.jpg";
//        string samsungProduct3 = "/images/samsungProduct3.jpg";

//        List<Image> images = new()
//        {
//            new Image{ Path = appleLogo},
//            new Image{ Path = bluettiLogo},
//            new Image{ Path = googlePixelLogo},
//            new Image{ Path = huaweiLogo},
//            new Image{ Path = samsungLogo},
//            new Image{ Path = appleProduct1},
//            new Image{ Path = bluettiProduct1},
//            new Image{ Path = bluettiProduct2},
//            new Image{ Path = googlePixelProduct1},
//            new Image{ Path = googlePixelProduct2},
//            new Image{ Path = googlePixelProduct3},
//            new Image{ Path = huaweiProduct1},
//            new Image{ Path = huaweiProduct2},
//            new Image{ Path = huaweiProduct3},
//            new Image{ Path = samsungProduct1},
//            new Image{ Path = samsungProduct2},
//            new Image{ Path = samsungProduct3},
//        };

//        await context.Images.AddRangeAsync(images);
//        await context.SaveChangesAsync();

//        List<Manufacturer> manufacturers = new()
//        {
//            new Manufacturer { Name = "Apple", Image = images[0]},
//            // todo create 4 manufacturers...
//        };

//        await context.AddRangeAsync(manufacturers);
//        await context.SaveChangesAsync();

//        List<Product> products = new()
//        {
//            new Product
//            {
//                Name = "Apple 16",
//                Description = "Tratata...",
//                Price = 100500.99M,
//                DiscountPercent = 5,
//                CreationDate = new DateTime(2024, 3, 10),
//                Rating = 3,
//                TopLevel = 9,
//                Images = [images[0]],
//                Manufacturer = manufacturers[0],
//            },
//            // todo create 11 products...
//        };

//        await context.Products.AddRangeAsync(products);
//        await context.SaveChangesAsync();
//    }
//}
