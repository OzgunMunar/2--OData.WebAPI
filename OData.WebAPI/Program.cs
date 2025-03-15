using Bogus;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using OData.WebAPI.Context;
using OData.WebAPI.Controllers;
using OData.WebAPI.Models;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options => {

    string connectionString = "Server=localhost,1433;Database=ODataTestDB;User Id=sa;Password=MyStrongPassword12345!;TrustServerCertificate=True";
    options.UseSqlServer(connectionString);

});

builder.Services.AddControllers().AddOData(options => {

    options
        .EnableQueryFeatures()
        .AddRouteComponents("odata", CategoryController.GetEdmModel());
    
});
builder.Services.AddOpenApi();

var app = builder.Build();

app.MapOpenApi();
app.MapScalarApiReference();

app.MapGet("seed-data/categories", async (ApplicationDbContext context) => {
   
    Faker faker = new();
    var categoryNames = faker.Commerce.Categories(100);
    List<Category> categories = categoryNames.Select(categoryName => new Category {
        Name = categoryName
    }).ToList();

    context.AddRange(categories);
    await context.SaveChangesAsync();

    return Results.NoContent();

}).Produces(204).WithTags("SeedProducts");

app.MapGet("seed-data/products", async (ApplicationDbContext context) => {
   
    Faker faker = new();
    var categories = context.Categories.ToList();
    List<Product> products = new();

    for(int i = 0; i < 10000; i++)
    {
        faker = new();
        Product product = new() 
        {
            CategoryId = categories[new Random().Next(categories.Count)].Id,
            Name = faker.Commerce.ProductName(),
            Price = Convert.ToDecimal(faker.Commerce.Price(100, 10000, 2))
        };
        products.Add(product);
    }

    context.AddRange(products);
    await context.SaveChangesAsync();

    return Results.NoContent();

});

app.MapControllers();

app.Run();
