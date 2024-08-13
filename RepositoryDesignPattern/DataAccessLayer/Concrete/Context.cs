
using Microsoft.EntityFrameworkCore;
using RepositoryDesignPattern.EntityLayer;

namespace RepositoryDesignPattern.DataAccessLayer.Concrete;

public class Context(DbContextOptions<Context> options) : DbContext(options)
{
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
}
