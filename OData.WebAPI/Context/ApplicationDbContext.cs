using Microsoft.EntityFrameworkCore;
using OData.WebAPI.Models;

namespace OData.WebAPI.Context;
public sealed class ApplicationDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Category> Categories => Set<Category>();

}