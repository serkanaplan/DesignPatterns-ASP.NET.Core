using UnitOfWorkDesignPattern.BusinessLayer.Abstract;
using UnitOfWorkDesignPattern.DataAccessLayer.Abstract;
using UnitOfWorkDesignPattern.DataAccessLayer.UnitOfWork;
using UnitOfWorkDesignPattern.EntityLayer;

namespace UnitOfWorkDesignPattern.BusinessLayer.Concrete;

public class CustomerManager(ICustomerDal customerDal, IUowDal uowDal) : ICustomerService
{
    private readonly ICustomerDal _customerDal = customerDal;
    private readonly IUowDal _uowDal = uowDal;

    public Customer TGetByID(int id) => _customerDal.GetByID(id);

    public List<Customer> TGetList() => _customerDal.GetList();

    public void TDelete(Customer t)
    {
        _customerDal.Delete(t);
        _uowDal.Save();
    }


    public void TInsert(Customer t)
    {
        _customerDal.Insert(t);
        _uowDal.Save();
    }


    public void TMultiUpdate(List<Customer> t)
    {
        _customerDal.MultiUpdate(t);
        _uowDal.Save();
    }


    public void TUpdate(Customer t)
    {
        _customerDal.Update(t);
        _uowDal.Save();
    }
}
