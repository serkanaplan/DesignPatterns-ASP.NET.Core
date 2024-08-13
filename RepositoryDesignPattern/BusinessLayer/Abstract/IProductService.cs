
using RepositoryDesignPattern.EntityLayer;

namespace RepositoryDesignPattern.BusinessLayer.Abstract;

public interface IProductService:IGenericService<Product>
{
    List<Product> TProductListWithCategory();
}
