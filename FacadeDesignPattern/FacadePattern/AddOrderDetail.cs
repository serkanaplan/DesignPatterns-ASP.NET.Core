using FacadeDesignPattern.DAL;

namespace FacadeDesignPattern.FacadePattern;

public class AddOrderDetail
{
    readonly Context context = new();
    public void AddNewOrderDetail(OrderDetail orderDetail)
    {
        context.OrderDetails.Add(orderDetail);
        context.SaveChanges();
    }
}
