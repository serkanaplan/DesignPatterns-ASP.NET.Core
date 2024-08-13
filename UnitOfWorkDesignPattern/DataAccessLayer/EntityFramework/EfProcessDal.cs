using System.Diagnostics;
using UnitOfWorkDesignPattern.DataAccessLayer.Abstract;
using UnitOfWorkDesignPattern.DataAccessLayer.Concrete;
using UnitOfWorkDesignPattern.DataAccessLayer.Repositories;

namespace UnitOfWorkDesignPattern.DataAccessLayer.EntityFramework;

public class EfProcessDal(Context context) : GenericRepository<Process>(context), IProcessDal
{
}
