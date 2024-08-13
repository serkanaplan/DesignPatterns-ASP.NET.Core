using Microsoft.EntityFrameworkCore;

namespace ChainOfResponsibiltyDesignPattern.DAL;

public class Context : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("server=ACER\\SQLEXPRESS;initial catalog=DesignPattern1;integrated security=true;TrustServerCertificate=True");
    }
    public DbSet<CustomerProcess> CustomerProcesses { get; set; }
}

