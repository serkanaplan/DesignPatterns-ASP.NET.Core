
using RepositoryDesignPattern.DataAccessLayer.Abstract;
using RepositoryDesignPattern.DataAccessLayer.Concrete;

namespace RepositoryDesignPattern.DataAccessLayer.Repositories;

public class GenericRepository<T>(Context context) : IGenericDal<T> where T : class
{
    private readonly Context _context = context;

    public T GetByID(int id) => _context.Set<T>().Find(id);

    public List<T> GetList() => [.. _context.Set<T>()];

    public void Delete(T t)
    {
        _context.Remove(t);
        _context.SaveChanges();
    }

    public void Insert(T t)
    {
        _context.Add(t);
        _context.SaveChanges();
    }

    public void Update(T t)
    {
        _context.Update(t);
        _context.SaveChanges();
    }
}
