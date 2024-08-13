using UnitOfWorkDesignPattern.DataAccessLayer.Abstract;
using UnitOfWorkDesignPattern.DataAccessLayer.Concrete;

namespace UnitOfWorkDesignPattern.DataAccessLayer.Repositories;

public class GenericRepository<T>(Context context) : IGenericDal<T> where T : class
{
    private readonly Context _context = context;

    public void Delete(T t) => _context.Remove(t);

    public T GetByID(int id) => _context.Set<T>().Find(id);

    public List<T> GetList() => [.. _context.Set<T>()];

    public void Insert(T t) => _context.Add(t);

    public void MultiUpdate(List<T> t) => _context.UpdateRange(t);

    public void Update(T t) => _context.Update(t);
}
