
using RepositoryDesignPattern.BusinessLayer.Abstract;
using RepositoryDesignPattern.DataAccessLayer.Abstract;
using RepositoryDesignPattern.EntityLayer;

namespace RepositoryDesignPattern.BusinessLayer.Concrete;

public class CategoryManager(ICategoryDal categoryDal) : ICategoryService
{
    private readonly ICategoryDal _categoryDal = categoryDal;

    public void TDelete(Category t) => _categoryDal.Delete(t);

    public Category TGetByID(int id) => _categoryDal.GetByID(id);

    public List<Category> TGetList() => _categoryDal.GetList();

    public void TInsert(Category t) => _categoryDal.Insert(t);

    public void TUpdate(Category t) => _categoryDal.Update(t);
}
