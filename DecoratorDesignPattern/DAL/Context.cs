using Microsoft.EntityFrameworkCore;

namespace DecoratorDesignPattern.DAL;

public class Context : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("server=ACER\\SQLEXPRESS;initial catalog=DesignPattern11;integrated security=true;TrustServerCertificate=True;");
    }
    public DbSet<Message> Messages { get; set; }
    public DbSet<Notifier> Notifiers { get; set; }
}
