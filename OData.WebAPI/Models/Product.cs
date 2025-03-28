namespace OData.WebAPI.Models;

public sealed class Product 
{
    public Product()
    {
        Id = Guid.NewGuid();
    }

    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public Guid CategoryId { get; set; } = default!;
    public Category? Category { get; set; }

    public decimal Price { get; set; }

}