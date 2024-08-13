using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using UnitOfWorkDesignPattern.BusinessLayer.Abstract;
using UnitOfWorkDesignPattern.EntityLayer;
using UnitOfWorkDesignPattern.Models;

namespace UnitOfWorkDesignPattern.Controllers;

public class HomeController(ICustomerService customerService) : Controller
{
    private readonly ICustomerService _customerService = customerService;

    [HttpGet]
    public IActionResult Index() => View();

    
    [HttpPost]
    public IActionResult Index(CustomerViewModel model)
    {

        var value1 = _customerService.TGetByID(model.SenderID);
        var value2 = _customerService.TGetByID(model.ReceiverID);

        value1.CustomerBalance -= model.Amount;
        value2.CustomerBalance += model.Amount;

        List<Customer> modifiedCustomers = [ value1, value2 ];

        _customerService.TMultiUpdate(modifiedCustomers);

        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
}
