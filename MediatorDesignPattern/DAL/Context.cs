using Microsoft.EntityFrameworkCore;

namespace MediatorDesignPattern.DAL;

public class Context : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("server=ACER\\SQLEXPRESS;initial catalog=DesignPattern8;integrated security=true;TrustServerCertificate=True;");
    }
    public DbSet<Product> Products { get; set; }
}
