using UnitOfWorkDesignPattern.DataAccessLayer.Concrete;

namespace UnitOfWorkDesignPattern.DataAccessLayer.UnitOfWork;

public class UowDal(Context context) : IUowDal
{
    private readonly Context _context = context;

    public void Save() => _context.SaveChanges();
}
