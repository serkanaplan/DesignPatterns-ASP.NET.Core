
using Microsoft.EntityFrameworkCore;
using RepositoryDesignPattern.DataAccessLayer.Abstract;
using RepositoryDesignPattern.DataAccessLayer.Concrete;
using RepositoryDesignPattern.DataAccessLayer.Repositories;
using RepositoryDesignPattern.EntityLayer;

namespace RepositoryDesignPattern.DataAccessLayer.EntityFramework
{
    public class EfProductDal(Context context) : GenericRepository<Product>(context), IProductDal
    {
        private readonly Context _context = context;

        public List<Product> ProductListWithCategory() => _context.Products.Include(x => x.Category).ToList();
    }
}
