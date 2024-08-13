using Microsoft.EntityFrameworkCore;
using UnitOfWorkDesignPattern.EntityLayer;

namespace UnitOfWorkDesignPattern.DataAccessLayer.Concrete;

public class Context(DbContextOptions<Context> options) : DbContext(options)
{
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Process> Processes { get; set; }
}
