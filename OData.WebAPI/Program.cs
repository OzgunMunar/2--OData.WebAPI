using Bogus;
using Microsoft.EntityFrameworkCore;
using OData.WebAPI.Context;
using OData.WebAPI.Models;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options => {

    string connectionString = "Server=localhost,1433;Database=ODataTestDB;User Id=sa;Password=MyStrongPassword12345!;TrustServerCertificate=True";
    options.UseSqlServer(connectionString);

});

builder.Services.AddControllers();
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

});

app.MapControllers();

app.Run();
