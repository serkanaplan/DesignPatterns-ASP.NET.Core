
using RepositoryDesignPattern.DataAccessLayer.Abstract;
using RepositoryDesignPattern.DataAccessLayer.Concrete;
using RepositoryDesignPattern.DataAccessLayer.Repositories;
using RepositoryDesignPattern.EntityLayer;

namespace RepositoryDesignPattern.DataAccessLayer.EntityFramework;

public class EfCategoryDal(Context context) : GenericRepository<Category>(context), ICategoryDal
{
}
