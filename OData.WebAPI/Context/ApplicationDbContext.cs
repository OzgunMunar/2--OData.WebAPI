using Microsoft.EntityFrameworkCore;
using OData.WebAPI.Models;

namespace OData.WebAPI.Context;
public sealed class ApplicationDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Product> Products => Set<Product>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>(builder => {
            builder.Property(p => p.Price).HasColumnType("money");
        });
    }

}