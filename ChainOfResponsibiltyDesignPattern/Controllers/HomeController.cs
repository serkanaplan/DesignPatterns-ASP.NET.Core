using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ChainOfResponsibiltyDesignPattern.Models;
using ChainOfResponsibiltyDesignPattern.ChainOfResponsibility;

namespace ChainOfResponsibiltyDesignPattern.Controllers;

public class HomeController : Controller
{
    [HttpGet]
    public IActionResult Index() => View();


    [HttpPost]
    public IActionResult Index(CustomerProcessViewModel model)
    {
        Employee treasurer = new Treasurer();
        Employee managerAssistant = new ManagerAssistant();
        Employee manager = new Manager();
        Employee areaDirector = new AreaDirector();

        treasurer.SetNextApprover(managerAssistant);
        managerAssistant.SetNextApprover(manager);
        manager.SetNextApprover(areaDirector);

        treasurer.ProcessRequest(model);
        return View();
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
}
