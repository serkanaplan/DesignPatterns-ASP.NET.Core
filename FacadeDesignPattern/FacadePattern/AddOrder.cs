using FacadeDesignPattern.DAL;

namespace FacadeDesignPattern.FacadePattern;

public class AddOrder
{
    readonly Context context = new();
    public void AddNewOrder(Order order)
    {
        order.OrderDate = DateTime.Parse(DateTime.Now.ToShortDateString());
        context.Orders.Add(order);
        context.SaveChanges();
    }
}
