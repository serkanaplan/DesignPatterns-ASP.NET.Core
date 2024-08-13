
using FacadeDesignPattern.DAL;

namespace FacadeDesignPattern.FacadePattern;

public class OrderFacade
{
    readonly Order order = new();
    readonly OrderDetail orderDetail = new();
    readonly ProductStock productStock = new();

    readonly AddOrder addOrder = new();
    readonly AddOrderDetail addOrderDetail = new();

    public void CompleteOrderDetail(int customerID, int productId, int orderID, int productCount, decimal productPrice)
    {

        orderDetail.OrderID = orderID;
        orderDetail.CustomerID = customerID;
        orderDetail.ProductID = productId;
        orderDetail.ProductCount = productCount;
        orderDetail.ProductPrice = productPrice;
        decimal totalProductPrice = productCount * productPrice;
        orderDetail.ProductTotalPrice = totalProductPrice;
        addOrderDetail.AddNewOrderDetail(orderDetail);

        productStock.StockDecrease(productId, productCount);
    }

    public void CompleteOrder(int customerID)
    {
        order.CustomerID = customerID;
        addOrder.AddNewOrder(order);
    }

}
