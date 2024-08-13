
using RepositoryDesignPattern.BusinessLayer.Abstract;
using RepositoryDesignPattern.DataAccessLayer.Abstract;
using RepositoryDesignPattern.EntityLayer;

namespace RepositoryDesignPattern.BusinessLayer.Concrete;

public class ProductManager(IProductDal productDal) : IProductService
{
    private readonly IProductDal _productDal = productDal;

    public void TDelete(Product t) => _productDal.Delete(t);

    public Product TGetByID(int id) => _productDal.GetByID(id);

    public List<Product> TGetList() => _productDal.GetList();

    public void TInsert(Product t) => _productDal.Insert(t);

    public List<Product> TProductListWithCategory() => _productDal.ProductListWithCategory();

    public void TUpdate(Product t) => _productDal.Update(t);
}
