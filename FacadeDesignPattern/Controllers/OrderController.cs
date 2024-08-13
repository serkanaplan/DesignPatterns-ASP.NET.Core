using FacadeDesignPattern.FacadePattern;
using Microsoft.AspNetCore.Mvc;

namespace FacadeDesignPattern.Controllers;

public class OrderController : Controller
{

    [HttpGet]
    public IActionResult OrderDetailStart() => View();


    [HttpPost]
    public IActionResult OrderDetailStart(int customerID, int productId, int orderID, int productCount, decimal productPrice)
    {
        OrderFacade order = new();
        order.CompleteOrderDetail(customerID, productId, orderID, productCount, productPrice);
        return RedirectToAction("Index");
    }


    [HttpGet]
    public IActionResult OrderStart() => View();


    [HttpPost]
    public IActionResult OrderStart(int customerID)
    {
        OrderFacade orderFacade = new();
        orderFacade.CompleteOrder(customerID);
        return RedirectToAction("Index");
    }


    public IActionResult Index() => View();
}
