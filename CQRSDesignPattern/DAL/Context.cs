
using Microsoft.EntityFrameworkCore;

namespace CQRSDesignPattern.DAL;

public class Context : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("server=ACER\\SQLEXPRESS;initial catalog=DesignPattern2;integrated security=true;TrustServerCertificate=True;");
    }

    public DbSet<Product> Products { get; set; }
}
