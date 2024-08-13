using UnitOfWorkDesignPattern.DataAccessLayer.Abstract;
using UnitOfWorkDesignPattern.DataAccessLayer.Concrete;
using UnitOfWorkDesignPattern.DataAccessLayer.Repositories;
using UnitOfWorkDesignPattern.EntityLayer;

namespace UnitOfWorkDesignPattern.DataAccessLayer.EntityFramework;

public class EfCustomerDal(Context context) : GenericRepository<Customer>(context), ICustomerDal
{
}
