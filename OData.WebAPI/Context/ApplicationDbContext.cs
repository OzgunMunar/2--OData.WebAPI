using Microsoft.EntityFrameworkCore;
using OData.WebAPI.Models;

namespace OData.WebAPI.Context;
public sealed class ApplicationDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<Product>(builder => {
            builder.Property(p => p.Price).HasColumnType("money");
        });

        modelBuilder.Entity<User>(builder => {
            builder.Property(p => p.UserType)
            .HasConversion(type => type.Value, value => UserTypeEnum.FromValue(value));
            builder.OwnsOne(p => p.Address);
        });

    }

}