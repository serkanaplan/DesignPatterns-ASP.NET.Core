using CompositeDesignPattern.Models;
using Microsoft.EntityFrameworkCore;

namespace CompositeDesignPattern.Data;


public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>()
            .HasMany(c => c.Components) // Category'nin Components ile ilişkisini tanımla
            .WithOne()
            .OnDelete(DeleteBehavior.Restrict); // Delete davranışını kısıtla (NO ACTION/RESTRICT)

        base.OnModelCreating(modelBuilder);
    }
}
