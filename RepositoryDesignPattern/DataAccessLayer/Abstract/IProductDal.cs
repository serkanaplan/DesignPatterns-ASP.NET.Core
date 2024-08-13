
using RepositoryDesignPattern.EntityLayer;

namespace RepositoryDesignPattern.DataAccessLayer.Abstract;

public interface IProductDal : IGenericDal<Product>
{
    List<Product> ProductListWithCategory();
}
